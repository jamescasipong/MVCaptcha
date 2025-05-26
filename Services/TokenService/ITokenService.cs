namespace MVCaptcha.Services
{
    public interface ITokenService
    {
        string GenerateToken(int sessionId, int currentIndex);
        bool ValidateToken(string token, out int sessionId, out int currentIndex);
    }
}