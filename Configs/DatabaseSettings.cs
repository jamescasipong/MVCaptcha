namespace MVCaptcha.Configs
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public int MaxRetryCount { get; set; } = 3;
        public int CommandTimeout { get; set; } = 30; // seconds
        public bool EnableSensitiveDataLogging { get; set; }
    }
}
