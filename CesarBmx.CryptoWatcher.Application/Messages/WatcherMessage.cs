

namespace CesarBmx.CryptoWatcher.Application.Messages
{
    public static class WatcherMessage
    {
        public const string WatcherNotFound = "The watcher does not exist";
        public const string WatcherAlreadyExists = "The watcher already exists";
        public const string WatcherAlreadyBought = "The watcher has bought already at {0}$";
        public const string WatcherAlreadySold = "The watcher has sold already at {0}$";
        public const string WatcherAlreadyLiquidated = "The watcher got liquidated already";
        public const string BuyLimitIsHigherThanWatcherValue = "The buy limit can't be higher than watcher's current value {0}$";
        public const string SellLimitIsLowerThanValue = "The sell limit can't be lower than watcher's current value {0}$";
    }
}
