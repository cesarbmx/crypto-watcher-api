using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class WatcherBuilder
    {
        public static WatcherStatus BuildStatus(decimal indicatorValue, WatcherSettings settings)
        {
            // Evaluate
            var watcherStatus = (indicatorValue >= settings.BuyAt) ? WatcherStatus.Buy : WatcherStatus.Sell;

            // Return
            return watcherStatus;
        }
    }
}
