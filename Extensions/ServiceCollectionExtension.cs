using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MVCaptcha.Configs;
using MVCaptcha.Data;
using MVCaptcha.Models.Repositories.CaptchaRepository;
using MVCaptcha.Models.Repositories.SessionRepository;
using MVCaptcha.Services;
using MVCaptcha.Services.Background;
using MVCaptcha.Services.CaptchaService;

namespace MVCaptcha.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddDataContext(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AppDataContext>((serviceProvider, options) =>
            {
                var dbSettings = serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value;

                options.UseMySQL(
                    dbSettings.ConnectionString,

                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(dbSettings.MaxRetryCount);
                        sqlOptions.CommandTimeout(dbSettings.CommandTimeout);
                    });
            });
        }

        public static void AddConfigSettings(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<DatabaseSettings>(configuration.GetSection("AppSettings:Database"));
            services.Configure<CaptchaSettings>(configuration.GetSection("AppSettings:Captcha"));
            services.Configure<JwtSettings>(configuration.GetSection("AppSettings:JwtSettings"));
        }

        public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<ICaptchaRepository, CaptchaRepository>();
        }

        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<SessionCleanupService>();

            services.AddScoped<ICaptchaService, CaptchaService>();
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
