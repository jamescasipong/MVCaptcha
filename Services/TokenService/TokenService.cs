using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MVCaptcha.Configs;
using Microsoft.Extensions.Logging;

namespace MVCaptcha.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<TokenService> _logger;

        public TokenService(IOptions<JwtSettings> jwtSettings, ILogger<TokenService> logger)
        {
            _jwtSettings = jwtSettings.Value;
            _logger = logger;
        }

        public string GenerateToken(int sessionId, int currentIndex)
        {
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim("sessionId", sessionId.ToString()),
                    new Claim("currentIndex", currentIndex.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
                    signingCredentials: creds
                );

                string jwt = new JwtSecurityTokenHandler().WriteToken(token);
                _logger.LogInformation("Generated token for session {SessionId} at index {CurrentIndex}", sessionId, currentIndex);
                return jwt;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate token for session {SessionId}", sessionId);
                throw;
            }
        }

        public bool ValidateToken(string token, out int sessionId, out int currentIndex)
        {
            sessionId = 0;
            currentIndex = 0;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            try
            {
                var parameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidAudience = _jwtSettings.Audience,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, parameters, out SecurityToken validatedToken);

                sessionId = int.Parse(principal.FindFirst("sessionId")?.Value ?? "0");
                currentIndex = int.Parse(principal.FindFirst("currentIndex")?.Value ?? "0");

                _logger.LogDebug("Validated token for session {SessionId} at index {CurrentIndex}", sessionId, currentIndex);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Token validation failed.");
                return false;
            }
        }
    }
}
