using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCaptcha.Models;
using MVCaptcha.Models.ViewModels;
using MVCaptcha.Services.CaptchaService;

namespace MVCaptcha.Controllers
{
    public class WelcomeController : Controller
    {
        private readonly ILogger<WelcomeController> _logger;
        private readonly ICaptchaService _captchaService;

        public WelcomeController(ILogger<WelcomeController> logger, ICaptchaService captchaService)
        {
            _logger = logger;
            _captchaService = captchaService;
        }

        public IActionResult Index()
        {
            return View(new WelcomeViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(WelcomeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var sessionId = await _captchaService.StartSession(model.SelectedDifficulty!, HttpContext);
            return RedirectToAction("Index", "Captcha", new { sessionId });
        }
    }
}
