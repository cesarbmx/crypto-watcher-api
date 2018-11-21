

namespace CryptoWatcher.Domain.Models
{
    public class Watcher : Entity
    {
        public WatcherType WatcherType { get; private set; }
        public WatcherSettings BuySellSettings { get; private set; }
        public WatcherSettings TrendSettings { get; private set; }

        public Watcher() { }
        public Watcher(
            string id, 
            WatcherType watcherType,
            WatcherSettings buySellSettings,
            WatcherSettings trendSettings)
        {
            Id = id;
            WatcherType = watcherType;
            BuySellSettings = buySellSettings;
            TrendSettings = trendSettings;
        }
    }
}
