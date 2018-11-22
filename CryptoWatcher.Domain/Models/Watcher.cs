

using CryptoWatcher.Shared.Helpers;

namespace CryptoWatcher.Domain.Models
{
    public class Watcher : Entity
    {      
        public string UserId { get; private set; }
        public string CurrencyId { get; private set; }
        public WatcherType WatcherType { get; private set; }
        public string WatcherId { get; private set; }
        public decimal CurrencyPrice { get; private set; }
        public WatcherSettings WatcherSettings { get; private set; }
        public WatcherSettings WatcherSettingsTrend { get; private set; }
        public bool Enabled { get; private set; }

        public Watcher() { }
        public Watcher(
            string userId, 
            string currencyId,
            WatcherType watcherType,
            decimal currencyPrice,
            WatcherSettings watcherSettings,
            WatcherSettings watcherSettingsTrend,
            bool enabled)
        {           
            UserId = userId;
            CurrencyId = currencyId;
            WatcherType = watcherType;
            WatcherId = UrlHelper.BuildUrl(userId, currencyId, watcherType.ToString()); // Semantic id
            CurrencyPrice = currencyPrice;
            WatcherSettings  = watcherSettings;
            WatcherSettingsTrend = watcherSettingsTrend;
            Enabled = enabled;
        }

        public Watcher UpdateSettings(WatcherSettings settings)
        {
            WatcherSettings = settings;

            return this;
        }
    }
}
