using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCaptcha.Models.Entities
{
    public class Session
    {
        [Key]
        [Column("session_id")]
        public int SessionId { get; set; }

        [Required(ErrorMessage = "Session start time is required.")]
        [Column("datetime_started")]
        public DateTime DateTimeStarted { get; set; } = DateTime.UtcNow;

        [Column("datetime_ended")]
        public DateTime? DateTimeEnded { get; set; }  // Nullable, optional

        [Required(ErrorMessage = "Difficulty level is required.")]
        [MaxLength(2, ErrorMessage = "Difficulty must be 1 or 2 characters (e.g., 'E', 'N', 'H').")]
        [RegularExpression("E|N|H", ErrorMessage = "Difficulty must be 'E', 'N', or 'H'.")]
        [Column("difficulty")]
        public string Difficulty { get; set; } = string.Empty;


        [Required(ErrorMessage = "Score is required.")]
        [MaxLength(5, ErrorMessage = "Score must not exceed 5 characters.")]
        [RegularExpression(@"^\d{1,5}$", ErrorMessage = "Score must be a number between 0 and 99999.")]
        [Column("score")]
        public string Score { get; set; } = string.Empty;
    }
}
