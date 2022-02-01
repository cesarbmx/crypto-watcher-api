


using System.Collections.Generic;
using CesarBmx.CryptoWatcher.Domain.Types;

namespace CesarBmx.CryptoWatcher.Application.Settings
{
    public  class AppSettings: CesarBmx.Shared.Application.Settings.AppSettings
    {
        public bool UseMemoryStorage { get; set; }
        public int JobsIntervalInMinutes { get; set; }
        public string TelegramApiToken { get; set; }
        public Dictionary<LinePeriod, string> Values { get; set; }
    }
}
