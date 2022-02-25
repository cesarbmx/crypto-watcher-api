using CesarBmx.CryptoWatcher.Application.ConflictReasons;
using CesarBmx.CryptoWatcher.Application.Messages;
using CesarBmx.CryptoWatcher.Application.Responses;

namespace CesarBmx.CryptoWatcher.Application.FakeResponses
{
    public static class FakeSetWatcherConflict
    {
        public static SetWatcherConflict GetFake()
        {
            return new SetWatcherConflict(SetWatcherConflictReason.BUY_LIMIT_MUST_BE_LOWER_THAN_WATCHER_VALUE, WatcherMessage.BuyLimitMustBeLowerThanWatcherValue);
        }
    }
}
