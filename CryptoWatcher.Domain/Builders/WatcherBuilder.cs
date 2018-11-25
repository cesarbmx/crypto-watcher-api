using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class WatcherBuilder
    {
        public static WatcherStatus BuildWatcherStatus(decimal indicatorValue, WatcherSettings watcherSettings)
        {
            // Evaluate
            var watcherStatus = (indicatorValue >= watcherSettings.BuyAt) ? WatcherStatus.Buy : WatcherStatus.Sell;

            // Return
            return watcherStatus;
        }
    }
}
