using Microsoft.AspNetCore.Mvc;
using MVCaptcha.Attributes;
using MVCaptcha.Models.ViewModels;
using MVCaptcha.Services.CaptchaService;

namespace MVCaptcha.Controllers
{
    public class CaptchaController : Controller
    {
        private readonly ICaptchaService _captchaService;

        public CaptchaController(ICaptchaService captchaService)
        {
            _captchaService = captchaService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int sessionId, int currentIndex = 0)
        {
            if (await(_captchaService.IsCompleted(sessionId)))
            {
                // Redirect to result page if session is already completed
                return RedirectToAction("Index", "Result", new { sessionId });
            }

            var session = await _captchaService.GetSessionId(sessionId);

            if (session == null)
            {
                // If session is not found, redirect to welcome page
                return RedirectToAction("Index", "Welcome");
            }

            // AWAIT the async operation to get the actual model
            var model = await _captchaService.GetNextCaptcha(sessionId, currentIndex);

            if (model == null)
                return RedirectToAction("Index", "Result", new { sessionId });

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CaptchaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // AWAIT the async validation
            var result = await _captchaService.ValidateCaptcha(
                model.SessionId,
                model.CurrentIndex,
                model.Answer);

            if (result.isComplete)
                return RedirectToAction("Index", "Result", new { sessionId = model.SessionId });

            return RedirectToAction("Index", new
            {
                sessionId = model.SessionId,
                currentIndex = model.CurrentIndex + 1
            });
        }
    }
}
