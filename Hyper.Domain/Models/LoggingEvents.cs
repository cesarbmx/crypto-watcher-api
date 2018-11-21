

namespace CryptoWatcher.Domain.Models
{
    public class LoggingEvents
    {
        public const string AllCurrenciesHaveBeenImported = "AllCurrenciesHaveBeenImported";
        public const string ImportingAllCurrenciesHasFailed = "ImportingAllCurrenciesHasFailed";
        public const string HypedCurrenciesHaveBeenSet = "HypedCurrenciesHaveBeenSet";
        public const string SettingHypedCurrenciesHasFailed = "SettingHypedCurrenciesHasFailed";
    }
}
