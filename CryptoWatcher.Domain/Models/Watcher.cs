

namespace CryptoWatcher.Domain.Models
{
    public class Watcher : Entity
    {
        public string WatcherId => Id;
        public string UserId { get; private set; }
        public WatcherType WatcherType { get; private set; }
        public string CurrencyId { get; private set; }
        public decimal CurrentValue { get; private set; }
        public WatcherSettings UserSettings { get; private set; }
        public WatcherSettings TrendSettings { get; private set; }
        public bool Enabled { get; private set; }
        public Watcher() { }
        public Watcher(
            string userId, 
            WatcherType type,
            string currencyId,
            decimal currentValue,
            WatcherSettings userSettings,
            WatcherSettings trendSettings,
            bool enabled)
        {
            UserId = userId;
            WatcherType = type;
            CurrencyId = currencyId;
            WatcherType = type;
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
