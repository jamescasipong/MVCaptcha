using Microsoft.AspNetCore.Mvc;
using MVCaptcha.Models.ViewModels;

namespace MVCaptcha.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var viewModel = new ErrorViewModel
            {
                StatusCode = statusCode,
                Message = statusCode switch
                {
                    404 => "The resource you requested could not be found",
                    500 => "An internal server error occurred",
                    _ => "An error occurred"
                }
            };

            return View("Error", viewModel);
        }
    }
}
