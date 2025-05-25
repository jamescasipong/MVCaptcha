namespace MVCaptcha.Models.ViewModels
{
    // Models/ViewModels/ErrorViewModel.cs
    public class ErrorViewModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string RequestId { get; set; } = string.Empty;
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
