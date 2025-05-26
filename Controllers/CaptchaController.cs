using Microsoft.AspNetCore.Mvc;
using MVCaptcha.Attributes;
using MVCaptcha.Models.ViewModels;
using MVCaptcha.Services;
using MVCaptcha.Services.CaptchaService;

namespace MVCaptcha.Controllers
{
    public class CaptchaController : Controller
    {
        private readonly ICaptchaService _captchaService;
        private readonly ITokenService _tokenService;
        private ILogger<CaptchaController> _logger;

        public CaptchaController(ICaptchaService captchaService, ITokenService tokenService, ILogger<CaptchaController> logger)
        {
            _captchaService = captchaService;
            _tokenService = tokenService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> Index(string token)
        {
            if (!_tokenService.ValidateToken(token, out int sessionId, out int currentIndex))
            {
                return RedirectToAction("Index", "Welcome");
            }

            if (await _captchaService.IsCompleted(sessionId))
            {
                return RedirectToAction("Index", "Result", new { sessionId });
            }

            var session = await _captchaService.GetSessionId(sessionId);

            if (session == null)
            {
                return RedirectToAction("Index", "Welcome");
            }

            var model = await _captchaService.GetNextCaptcha(sessionId, currentIndex);
            if (model == null)
            {
                return RedirectToAction("Index", "Result", new { sessionId });
            }

            model.Token = token; // store token in the model for POST

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Index(CaptchaViewModel model)
        {
            if (!_tokenService.ValidateToken(model.Token, out int sessionId, out int currentIndex))
            {
                _logger.LogWarning("Invalid model state or token validation failed. Redirecting to Welcome page.");
                return RedirectToAction("Index", "Welcome");
            }

            var result = await _captchaService.ValidateCaptcha(sessionId, currentIndex, model.Answer);

            if (result.isComplete)
                return RedirectToAction("Index", "Result", new { sessionId });

            // Generate a new token with the updated index
            string token = _tokenService.GenerateToken(sessionId, currentIndex + 1);

            return RedirectToAction("Index", new { token });

        }

    }
}
