using MVCaptcha.Models.Entities;
using MVCaptcha.Models.Repositories.CaptchaRepository;
using MVCaptcha.Models.Repositories.SessionRepository;
using MVCaptcha.Models.ViewModels;
using System.Collections.Concurrent;

namespace MVCaptcha.Services.CaptchaService
{
    // Services/CaptchaService.cs
    public class CaptchaService : ICaptchaService
    {
        private readonly ICaptchaRepository _captchaRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly ConcurrentDictionary<int, List<int>> _sessionCaptchas = new();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CaptchaService> _logger;

        public CaptchaService(ICaptchaRepository captchaRepository, ISessionRepository sessionRepository, IHttpContextAccessor httpContextAccessor, ILogger<CaptchaService> logger)
        {
            _captchaRepository = captchaRepository;
            _sessionRepository = sessionRepository;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<int> StartSession(string difficulty, HttpContext httpContext)
        {
            if (string.IsNullOrWhiteSpace(difficulty))
                throw new ArgumentException("Difficulty must be provided", nameof(difficulty));

            // 1. Save the session to DB
            var sessionId = await _sessionRepository.CreateSession(difficulty);

            // 2. Store session ID in the user’s HTTP session
            httpContext.Session.SetInt32("SessionId", sessionId);

            // 3. Optionally: preload captchas for this session (without answers)
            var captchas = await _captchaRepository.GetByDifficulty(difficulty);
            var captchaIds = captchas.Select(c => c.Id).ToList();

            // Store them securely in memory (can be moved to DB if needed)
            _sessionCaptchas[sessionId] = captchaIds;

            return sessionId;
        }

        public async Task<CaptchaViewModel> GetNextCaptcha(int sessionId, int currentIndex)
        {
            if (!_sessionCaptchas.TryGetValue(sessionId, out var captchaIds))
            {
                var session = await _sessionRepository.GetByIdAsync(sessionId); // assuming this is async too
                var captchas = await _captchaRepository.GetByDifficulty(session.Difficulty);

                captchaIds = captchas.Select(c => c.Id).ToList();
                _sessionCaptchas[sessionId] = captchaIds;
            }

            if (currentIndex < 0 || currentIndex >= captchaIds.Count)
                throw new ArgumentOutOfRangeException(nameof(currentIndex), "Invalid captcha index.");

            var captcha = await _captchaRepository.GetByIdAsync(captchaIds[currentIndex]);


            return new CaptchaViewModel
            {
                SessionId = sessionId,
                CurrentIndex = currentIndex,
                TotalCount = captchaIds.Count,
                ImageUrl = captcha.ImageUrl,
                AnswerLength = captcha.CaptchaValue.Length
            };
        }


        public async Task<(bool isValid, bool isComplete)> ValidateCaptcha(int sessionId, int currentIndex, string answer)
        {
            if (!_sessionCaptchas.TryGetValue(sessionId, out var captchaIds))
            {
                // Recover by fetching and re-caching the session
                var session = await _sessionRepository.GetByIdAsync(sessionId);
                if (session == null)
                    throw new InvalidOperationException("Session not found.");

                var captchas = await _captchaRepository.GetByDifficulty(session.Difficulty);
                captchaIds = captchas.Select(c => c.Id).ToList();
                _sessionCaptchas[sessionId] = captchaIds;
            }

            if (currentIndex < 0 || currentIndex >= captchaIds.Count)
                throw new ArgumentOutOfRangeException(nameof(currentIndex), "Invalid captcha index.");

            var captcha = await _captchaRepository.GetByIdAsync(captchaIds[currentIndex]);
            bool isCorrect = string.Equals(answer, captcha.CaptchaValue, StringComparison.OrdinalIgnoreCase);

            _httpContextAccessor.HttpContext?.Session.SetInt32($"Captcha_{sessionId}_{currentIndex}", isCorrect ? 1 : 0);

            bool isComplete = currentIndex + 1 >= captchaIds.Count;
            if (isComplete)
            {
                int score = 0;
                for (int i = 0; i < captchaIds.Count; i++)
                {
                    score += _httpContextAccessor.HttpContext?.Session.GetInt32($"Captcha_{sessionId}_{i}") ?? 0;
                }


                await _sessionRepository.CompleteSession(sessionId, score);
                _sessionCaptchas.TryRemove(sessionId, out _);
            }

            return (isCorrect, isComplete);
        }


        public async Task<ResultViewModel> GetResult(int sessionId)
        {
            var session = await _sessionRepository.GetByIdAsync(sessionId);

            if (session == null || session.DateTimeEnded == null)
            {
                _logger.LogWarning("Session {SessionId} not found or not completed.", sessionId);
                return null;
            }

            return new ResultViewModel
            {
                Score = int.Parse(session.Score),
                Total = 3,
                Duration = (session.DateTimeEnded!.Value - session.DateTimeStarted),
                Difficulty = session.Difficulty == "E" ? "Easy" :
                            session.Difficulty == "N" ? "Normal" : "Hard",
            };
        }

        public async Task<bool> IsCompleted(int sessionId)
        {
            var session = await  _sessionRepository.GetByIdAsync(sessionId);

            if (session.DateTimeEnded == null)
            {
                // Session is not completed yet
                return false;
            }
            else
            {
                // Session is completed
                return true;
            }
        }

        public async Task<Session> GetSessionId(int sessionId)
        {
            try
            {
                return await _sessionRepository.GetByIdAsync(sessionId);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

}
