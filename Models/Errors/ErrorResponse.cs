namespace MVCaptcha.Models.Errors
{
    public class ErrorResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public Dictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
        public string StackTrace { get; set; } = string.Empty;
    }
}
