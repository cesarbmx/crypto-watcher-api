using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class WatcherBuilder
    {
        public static OperationType BuildOperationType(decimal indicatorValue, WatcherSettings watcherSettings)
        {
            // Evaluate
            var watcherStatus = (indicatorValue >= watcherSettings.BuyAt) ? OperationType.Buy : OperationType.Sell;

            // Return
            return watcherStatus;
        }
    }
}
