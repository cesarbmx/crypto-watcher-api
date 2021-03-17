


namespace CesarBmx.CryptoWatcher.Application.Settings
{
    public  class AppSettings: CesarBmx.Shared.Application.Settings.AppSettings
    {
        public bool Authorize { get; set; }
        public bool UseMemoryStorage { get; set; }
        public int JobsIntervalInMinutes { get; set; }
        public string TelegramApiToken { get; set; }
    }
}
