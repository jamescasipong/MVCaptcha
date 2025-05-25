namespace MVCaptcha.Models.ViewModels
{
    public class DifficultyLevelViewModel
    {
        public string Value { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;  // your icon CSS class, e.g. "fas fa-star"
        public bool Selected { get; set; }
    }

}
