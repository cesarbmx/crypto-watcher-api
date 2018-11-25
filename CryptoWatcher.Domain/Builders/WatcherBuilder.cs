using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class WatcherBuilder
    {
        public static bool BuildWatcherStatus(WatcherSettings watcherSettings, decimal watcherValue)
        {
            // Return
            return watcherValue >= watcherSettings.BuyAt;
        }
    }
}
