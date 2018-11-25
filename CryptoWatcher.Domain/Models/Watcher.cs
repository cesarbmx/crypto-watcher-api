using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Shared.Helpers;


namespace CryptoWatcher.Domain.Models
{
    public class Watcher : Entity
    {
        public string WatcherId { get; private set; }
        public string UserId { get; private set; }
        public Indicator IndicatorId { get; private set; }
        public string CurrencyId { get; private set; }
        public decimal WatcherValue { get; private set; }
        public WatcherSettings WatcherSettings { get; private set; }
        public WatcherSettings WatcherSettingsTrend { get; private set; }
        public bool WatcherEnabled { get; private set; }
        public bool WatcherStatus => WatcherBuilders.BuildWatcherStatus(WatcherSettings, WatcherValue);

        public Watcher() { }
        public Watcher(
            string userId, 
            Indicator indicatorId,
            string currencyId,
            decimal watcherValue,
            WatcherSettings watcherSettings,
            WatcherSettings watcherSettingsTrend,
            bool enabled)
        {
            WatcherId = UrlHelper.BuildUrl(userId, currencyId, indicatorId.ToString()); // Semantic id
            UserId = userId;
            IndicatorId = indicatorId;
            CurrencyId = currencyId;
            WatcherValue = watcherValue;
            WatcherSettings  = watcherSettings;
            WatcherSettingsTrend = watcherSettingsTrend;
            WatcherEnabled = enabled;
        }

        public Watcher UpdateSettings(WatcherSettings settings)
        {
            WatcherSettings = settings;

            return this;
        }
    }
}
