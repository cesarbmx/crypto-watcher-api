

namespace CryptoWatcher.Domain.Models
{
    public class Watcher : Entity
    {
        public WatcherType WatcherType { get; private set; }
        public decimal CurrentValue { get; private set; }
        public WatcherSettings Settings { get; private set; }
        public WatcherSettings TrendSettings { get; private set; }
        public Watcher() { }
        public Watcher(
            string id, 
            WatcherType watcherType,
            decimal currentValue,
            WatcherSettings settings,
            WatcherSettings trendSettings)
        {
            Id = id;
            WatcherType = watcherType;
            CurrentValue = currentValue;
            Settings  = settings;
            TrendSettings = trendSettings;
        }
    }
}
