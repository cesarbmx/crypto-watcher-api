using CesarBmx.Notification.Domain.Models;

namespace CesarBmx.Notification.Application.Settings
{
    public class AppSettings : Shared.Application.Settings.AppSettings
    {
        public bool UseMemoryStorage { get; set; }
        public int JobsIntervalInMinutes { get; set; }
        public string TelegramApiToken { get; set; }
        public LineRetention LineRetention { get; set; }
    }
}
