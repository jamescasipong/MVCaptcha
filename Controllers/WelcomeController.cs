using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCaptcha.Attributes;
using MVCaptcha.Exceptions;
using MVCaptcha.Models;
using MVCaptcha.Models.ViewModels;
using MVCaptcha.Services.CaptchaService;

namespace MVCaptcha.Controllers
{
    public class WelcomeController : Controller
    {
        private readonly ICaptchaService _captchaService;

        public WelcomeController(ILogger<WelcomeController> logger, ICaptchaService captchaService)
        {
            _captchaService = captchaService;
        }

        public IActionResult Index()
        {
            return View(new WelcomeViewModel());
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Index(WelcomeViewModel model)
        {
            var token = await _captchaService.StartSessionToken(model.SelectedDifficulty!);
            return RedirectToAction("Index", "Captcha", new { token });
        }
    }
}
