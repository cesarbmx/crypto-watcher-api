using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Shared.Helpers;


namespace CryptoWatcher.Domain.Models
{
    public class Watcher : Entity
    {
        public string UserId { get; private set; }
        public string CurrencyId { get; private set; }
        public IndicatorType IndicatorType { get; private set; }
        public decimal IndicatorValue { get; private set; }
        public WatcherSettings Settings { get; private set; }
        public WatcherSettings SettingsTrend { get; private set; }
        public bool Enabled { get; private set; }
        public WatcherStatus Status => WatcherBuilder.BuildStatus(IndicatorValue, Settings);

        public Watcher()
        {
            Settings = new WatcherSettings();
            SettingsTrend = new WatcherSettings();
        }
        public Watcher(
            string userId,
            string currencyId,
            IndicatorType indicatorType,
            decimal indicatorValue,
            WatcherSettings settings,
            WatcherSettings settingsTrend,
            bool watcherIsEnabled)
        {
            UserId = UrlHelper.BuildUrl(userId, currencyId, indicatorType.ToString()); // Semantic id
            UserId = userId;
            CurrencyId = currencyId;
            IndicatorType = indicatorType;
            IndicatorValue = indicatorValue;
            Settings = settings;
            SettingsTrend = settingsTrend;
            Enabled = watcherIsEnabled;
        }

        public Watcher UpdateSettings(WatcherSettings settings)
        {
            Settings = settings;

            return this;
        }
    }
}
