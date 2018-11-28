using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Shared.Helpers;


namespace CryptoWatcher.Domain.Models
{
    public class Watcher : Entity
    {
        public string WatcherId { get; private set; }
        public string UserId { get; private set; }
        public string CurrencyId { get; private set; }
        public IndicatorType IndicatorType { get; private set; }
        public decimal IndicatorValue { get; private set; }
        public WatcherSettings WatcherSettings { get; private set; }
        public WatcherSettings WatcherSettingsTrend { get; private set; }
        public bool WatcherIsEnabled { get; private set; }
        public WatcherStatus WatcherStatus => WatcherBuilder.BuildWatcherStatus(IndicatorValue, WatcherSettings);

        public Watcher()
        {
            WatcherSettings = new WatcherSettings();
            WatcherSettingsTrend = new WatcherSettings();
        }
        public Watcher(
            string userId,
            string currencyId,
            IndicatorType indicatorType,
            decimal indicatorValue,
            WatcherSettings watcherSettings,
            WatcherSettings watcherSettingsTrend,
            bool watcherIsEnabled)
        {
            WatcherId = UrlHelper.BuildUrl(userId, currencyId, indicatorType.ToString()); // Semantic id
            UserId = userId;
            CurrencyId = currencyId;
            IndicatorType = indicatorType;
            IndicatorValue = indicatorValue;
            WatcherSettings  = watcherSettings;
            WatcherSettingsTrend = watcherSettingsTrend;
            WatcherIsEnabled = watcherIsEnabled;
        }

        public Watcher UpdateSettings(WatcherSettings settings)
        {
            WatcherSettings = settings;

            return this;
        }
    }
}
