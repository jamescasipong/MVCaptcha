using System.ComponentModel.DataAnnotations;

namespace MVCaptcha.Models.ViewModels
{
    public class CaptchaViewModel
    {
        public int SessionId { get; set; }
        public int CurrentIndex { get; set; }
        public int TotalCount { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter the captcha code")]
        [StringLength(10, ErrorMessage = "Code must be {1} characters", MinimumLength = 4)]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Only letters and numbers allowed")]
        public string Answer { get; set; } = string.Empty;

        // Helper property for client-side validation
        //public int ExpectedLength => ImageUrl.Contains("hard") ? 6 :
        //                           ImageUrl.Contains("normal") ? 5 : 4;

        // Add alias property to match the Razor view
        public int AnswerLength { get; set; } = 4; // Default to 4, can be adjusted based on difficulty
    }
}
