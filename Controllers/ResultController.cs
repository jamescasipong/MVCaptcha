using Microsoft.AspNetCore.Mvc;
using MVCaptcha.Services.CaptchaService;

namespace MVCaptcha.Controllers
{
    public class ResultController : Controller
    {
        private readonly ICaptchaService _captchaService;

        public ResultController(ICaptchaService captchaService)
        {
            _captchaService = captchaService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int sessionId)
        {
            var model = await _captchaService.GetResult(sessionId);
            if (model == null)
                return RedirectToAction("Index", "Welcome");

            return View(model);
        }
    }
}
