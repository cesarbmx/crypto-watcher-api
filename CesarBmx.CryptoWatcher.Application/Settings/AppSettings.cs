using CesarBmx.CryptoWatcher.Domain.Models;

namespace CesarBmx.CryptoWatcher.Application.Settings
{
    public  class AppSettings
    {
        public bool UseMemoryStorage { get; set; }
        public int JobsIntervalInMinutes { get; set; }
        public string TelegramApiToken { get; set; }
        public LineRetention LineRetention { get; set; }
    }
}
