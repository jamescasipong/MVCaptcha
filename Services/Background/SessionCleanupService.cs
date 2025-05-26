using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MVCaptcha.Models.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MVCaptcha.Data;
using MVCaptcha.Configs;
using Microsoft.Extensions.Options;

namespace MVCaptcha.Services.Background
{
    public class SessionCleanupService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SessionCleanupService> _logger;
        private readonly JwtSettings _jwtSettings;
        private const int CleanupIntervalMinutes = 5;

        public SessionCleanupService(IServiceProvider serviceProvider, IOptions<JwtSettings> jwtSettings, ILogger<SessionCleanupService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _jwtSettings = jwtSettings.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("SessionCleanupService started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<AppDataContext>();

                        var tenMinutesAgo = DateTime.UtcNow.AddMinutes(-_jwtSettings.ExpirationMinutes);

                        var expiredSessions = await dbContext.Sessions
                            .Where(s => s.DateTimeEnded == null && s.DateTimeStarted <= tenMinutesAgo)
                            .ToListAsync(stoppingToken);

                        if (expiredSessions.Any())
                        {
                            dbContext.Sessions.RemoveRange(expiredSessions);
                            await dbContext.SaveChangesAsync(stoppingToken);
                            _logger.LogInformation($"Deleted {expiredSessions.Count} expired session(s).");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while cleaning up sessions.");
                }

                await Task.Delay(TimeSpan.FromMinutes(CleanupIntervalMinutes), stoppingToken);
            }
        }
    }
}
