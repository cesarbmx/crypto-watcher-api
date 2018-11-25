using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Shared.Helpers;


namespace CryptoWatcher.Domain.Models
{
    public class Watcher : Entity
    {      
        public string UserId { get; private set; }
        public WatcherType WatcherType { get; private set; }
        public string CurrencyId { get; private set; }
        public string WatcherId { get; private set; }
        public decimal WatcherValue { get; private set; }
        public WatcherSettings WatcherSettings { get; private set; }
        public WatcherSettings WatcherSettingsTrend { get; private set; }
        public bool WatcherEnabled { get; private set; }
        public bool WatcherStatus => WatcherBuilders.BuildWatcherStatus(WatcherSettings.BuyAt, WatcherValue);

        public Watcher() { }
        public Watcher(
            string userId, 
            WatcherType watcherType,
            string currencyId,
            decimal watcherValue,
            WatcherSettings watcherSettings,
            WatcherSettings watcherSettingsTrend,
            bool enabled)
        {           
            UserId = userId;
            WatcherType = watcherType;
            CurrencyId = currencyId;
            WatcherId = UrlHelper.BuildUrl(userId, currencyId, watcherType.ToString()); // Semantic id
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
