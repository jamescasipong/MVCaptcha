using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCaptcha.Models.Entities
{
    public class Captcha
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Captcha name is required.")]
        [MaxLength(45, ErrorMessage = "Captcha name must not exceed 45 characters.")]
        [Column("captcha_name")]
        public string CaptchaName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Captcha value is required.")]
        [MaxLength(45, ErrorMessage = "Captcha value must not exceed 45 characters.")]
        [Column("captcha_value")]
        public string CaptchaValue { get; set; } = string.Empty;

        [Required(ErrorMessage = "Image URL is required.")]
        [MaxLength(200, ErrorMessage = "Image URL must not exceed 200 characters.")]
        [Column("image_url")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Level is required.")]
        [MaxLength(2, ErrorMessage = "Level must be 1 or 2 characters (E, N, or H).")]
        [RegularExpression("E|N|H", ErrorMessage = "Level must be 'E', 'N', or 'H'.")]
        [Column("level")]
        public string Level { get; set; } = string.Empty;
    }
}
