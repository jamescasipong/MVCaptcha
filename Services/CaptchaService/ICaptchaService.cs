using MVCaptcha.Models.Entities;
using MVCaptcha.Models.ViewModels;

namespace MVCaptcha.Services.CaptchaService
{
    public interface ICaptchaService
    {
        Task<string> StartSessionToken(string difficulty);
        Task<Session> GetSessionId(int sessionId);
        Task<CaptchaViewModel> GetNextCaptcha(int sessionId, int currentIndex);
        Task<(bool isValid, bool isComplete)> ValidateCaptcha(int sessionId, int currentIndex, string answer);
        Task<bool> IsCompleted(int sessionId);
        Task<ResultViewModel> GetResult(int sessionId);
    }
}
