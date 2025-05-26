using MVCaptcha.Exceptions;
using MVCaptcha.Models.Entities;
using MVCaptcha.Models.Repositories.CaptchaRepository;
using MVCaptcha.Models.Repositories.SessionRepository;
using MVCaptcha.Models.ViewModels;
using System.Collections.Concurrent;

namespace MVCaptcha.Services.CaptchaService
{
    public class CaptchaService : ICaptchaService
    {
        private readonly ICaptchaRepository _captchaRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly ITokenService _tokenService;
        private readonly ILogger<CaptchaService> _logger;

        // In-memory state
        private readonly ConcurrentDictionary<int, List<int>> _sessionCaptchas = new();
        private readonly ConcurrentDictionary<int, Dictionary<int, bool>> _captchaAnswers = new();

        public CaptchaService(
            ICaptchaRepository captchaRepository,
            ISessionRepository sessionRepository,
            ITokenService tokenService,
            ILogger<CaptchaService> logger)
        {
            _captchaRepository = captchaRepository;
            _sessionRepository = sessionRepository;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<string> StartSessionToken(string difficulty)
        {
            if (string.IsNullOrWhiteSpace(difficulty))
                throw new BadRequestException("Difficulty must be provided");

            var sessionId = await _sessionRepository.CreateSession(difficulty);
            var captchas = await _captchaRepository.GetByDifficulty(difficulty);
            var captchaIds = captchas.Select(c => c.Id).ToList();

            _sessionCaptchas[sessionId] = captchaIds;
            _captchaAnswers[sessionId] = new Dictionary<int, bool>();

            var token = _tokenService.GenerateToken(sessionId, 0);
            return token;
        }

        public async Task<CaptchaViewModel> GetNextCaptcha(int sessionId, int currentIndex)
        {
            if (!_sessionCaptchas.TryGetValue(sessionId, out var captchaIds))
            {
                var session = await _sessionRepository.GetByIdAsync(sessionId);
                var captchas = await _captchaRepository.GetByDifficulty(session.Difficulty);
                captchaIds = captchas.Select(c => c.Id).ToList();
                _sessionCaptchas[sessionId] = captchaIds;
            }

            if (currentIndex < 0 || currentIndex >= captchaIds.Count)
                throw new BadRequestException("Invalid captcha index.");

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
                var session = await _sessionRepository.GetByIdAsync(sessionId);
                if (session == null)
                    throw new NotFoundException("Session not found.");

                var captchas = await _captchaRepository.GetByDifficulty(session.Difficulty);
                captchaIds = captchas.Select(c => c.Id).ToList();
                _sessionCaptchas[sessionId] = captchaIds;
            }

            if (currentIndex < 0 || currentIndex >= captchaIds.Count)
                throw new BadRequestException("Invalid captcha index.");

            var captcha = await _captchaRepository.GetByIdAsync(captchaIds[currentIndex]);
            bool isCorrect = string.Equals(answer, captcha.CaptchaValue);

            if (!_captchaAnswers.TryGetValue(sessionId, out var answerMap))
            {
                answerMap = new Dictionary<int, bool>();
                _captchaAnswers[sessionId] = answerMap;
            }

            answerMap[currentIndex] = isCorrect;

            bool isComplete = currentIndex + 1 >= captchaIds.Count;
            if (isComplete)
            {
                int score = answerMap.Count(kvp => kvp.Value);

                await _sessionRepository.CompleteSession(sessionId, score);
                _sessionCaptchas.TryRemove(sessionId, out _);
                _captchaAnswers.TryRemove(sessionId, out _);
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
            var session = await _sessionRepository.GetByIdAsync(sessionId);
            return session?.DateTimeEnded != null;
        }

        public async Task<Session> GetSessionId(int sessionId)
        {
            try
            {
                return await _sessionRepository.GetByIdAsync(sessionId);
            }
            catch
            {
                return null;
            }
        }
    }
}
