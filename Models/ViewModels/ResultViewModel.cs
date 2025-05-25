namespace MVCaptcha.Models.ViewModels
{
    public class ResultViewModel
    {
        public int Score { get; set; }
        public int Total { get; set; }
        public TimeSpan Duration { get; set; }
        public string Difficulty { get; set; } = string.Empty;
    }
}
