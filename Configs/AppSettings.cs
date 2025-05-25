namespace MVCaptcha.Configs
{
    public class AppSettings
    {
        public DatabaseSettings Database { get; set; } = new DatabaseSettings();
        public CaptchaSettings Captcha { get; set; } = new CaptchaSettings();
    }
}
