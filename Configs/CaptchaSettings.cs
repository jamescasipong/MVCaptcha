namespace MVCaptcha.Configs
{
    public class CaptchaSettings
    {
        public int ExpiryMinutes { get; set; }
        public string ImageFolder { get; set; } = string.Empty;
        public int CaptchaCount { get; set; }
    }
}
