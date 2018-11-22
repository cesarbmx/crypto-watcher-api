

namespace CryptoWatcher.Domain.Models
{
    public class Watcher : Entity
    {
        public string UserId { get; private set; }
        public WatcherType Type { get; private set; }
        public string CurrencyId { get; private set; }
        public decimal CurrentValue { get; private set; }
        public WatcherSettings Settings { get; private set; }
        public WatcherSettings TrendSettings { get; private set; }
        public bool Enabled { get; private set; }
        public Watcher() { }
        public Watcher(
            string userId, 
            WatcherType type,
            string currencyId,
            decimal currentValue,
            WatcherSettings settings,
            WatcherSettings trendSettings,
            bool enabled)
        {
            UserId = userId;
            Type = type;
            CurrencyId = currencyId;
            Type = type;
            CurrentValue = currentValue;
            Settings  = settings;
            TrendSettings = trendSettings;
            Enabled = enabled;
        }

        public Watcher UpdateSettings(WatcherSettings settings)
        {
            Settings = settings;

            return this;
        }
    }
}
