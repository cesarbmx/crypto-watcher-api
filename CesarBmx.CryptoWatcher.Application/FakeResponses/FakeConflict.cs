using CesarBmx.CryptoWatcher.Application.ConflictReasons;
using CesarBmx.CryptoWatcher.Application.Messages;
using CesarBmx.CryptoWatcher.Application.Responses;

namespace CesarBmx.CryptoWatcher.Application.FakeResponses
{
    public static class FakeConflict
    {
        public static SetWatcherConflict GetFake_SetWatcherConflict()
        {
            return new SetWatcherConflict( SetWatcherConflictReason.BUY_LIMIT_MUST_BE_LOWER_THAN_WATCHER_VALUE, string.Format(WatcherMessage.BuyLimitMustBeLowerThanWatcherValue, 3000));
        }
    }
}
