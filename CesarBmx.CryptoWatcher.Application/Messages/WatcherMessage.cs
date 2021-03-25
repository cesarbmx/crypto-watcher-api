

namespace CesarBmx.CryptoWatcher.Application.Messages
{
    public static class WatcherMessage
    {
        public const string WatcherNotFound = "The watcher does not exist";
        public const string WatcherAlreadyExists = "The watcher already exists";
        public const string WatcherAlreadyBought = "The watcher has bought already at {0}$";
        public const string WatcherAlreadySold = "The watcher has sold already at {0}$";
        public const string WatcherAlreadyLiquidated = "The watcher got liquidated already";
        public const string BuyLimitMustBeLowerThanWatcherValue = "The buy limit must be lower than the watcher's current value {0}$";
        public const string SellLimitMustBeHigherThanWatcherValue = "The sell limit must be higher than the watcher's current value {0}$";
        public const string SellLimitMustBeHigherThanBuyLimit = "The sell limit must be higher than the buy limit";
        public const string BuyLimitMustBeHigherThanZero = "The buy limit must be higher than zero";
    }
}
