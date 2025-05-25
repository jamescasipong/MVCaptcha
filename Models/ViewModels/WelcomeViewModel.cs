using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MVCaptcha.Models.ViewModels
{
    public class WelcomeViewModel
    {
        [Required]
        public string? SelectedDifficulty { get; set; } = "N";

        public List<DifficultyLevelViewModel> DifficultyLevels { get; set; } = new List<DifficultyLevelViewModel>
        {
            new DifficultyLevelViewModel { Text = "Easy", Value = "E", Icon = "smile", Selected = false },
            new DifficultyLevelViewModel { Text = "Normal", Value = "N", Icon = "meh", Selected = false },
            new DifficultyLevelViewModel { Text = "Hard", Value = "H", Icon = "frown", Selected = false }
        };
    }

}
