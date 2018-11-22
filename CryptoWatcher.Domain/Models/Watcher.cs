

using CryptoWatcher.Shared.Helpers;

namespace CryptoWatcher.Domain.Models
{
    public class Watcher : Entity
    {
        public string WatcherId { get; private set; }
        public string UserId { get; private set; }
        public string CurrencyId { get; private set; }
        public WatcherType Type { get; private set; }
        public decimal CurrentValue { get; private set; }
        public WatcherSettings UserSettings { get; private set; }
        public WatcherSettings TrendSettings { get; private set; }
        public bool Enabled { get; private set; }
        public Watcher() { }
        public Watcher(
            string userId, 
            string currencyId,
            WatcherType type,
            decimal currentValue,
            WatcherSettings userSettings,
            WatcherSettings trendSettings,
            bool enabled)
        {
            WatcherId = UrlHelper.BuildUrl(userId, currencyId, type.ToString()); // Semantic id
            UserId = userId;
            CurrencyId = currencyId;
            Type = type;
            CurrentValue = currentValue;
            UserSettings  = userSettings;
            TrendSettings = trendSettings;
            Enabled = enabled;
        }

        public Watcher UpdateSettings(WatcherSettings settings)
        {
            UserSettings = settings;

            return this;
        }
    }
}
