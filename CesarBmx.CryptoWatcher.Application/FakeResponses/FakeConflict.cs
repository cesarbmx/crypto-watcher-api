using CesarBmx.CryptoWatcher.Application.ConflictReasons;
using CesarBmx.CryptoWatcher.Application.Messages;
using CesarBmx.Shared.Application.Responses;

namespace CesarBmx.CryptoWatcher.Application.FakeResponses
{
    public static class FakeConflict
    {
        public static Conflict<SetWatcherConflictReason> GetFake_SetWatcherConflict()
        {
            return new Conflict<SetWatcherConflictReason>( SetWatcherConflictReason.BUY_LIMIT_MUST_BE_LOWER_THAN_WATCHER_VALUE, WatcherMessage.BuyLimitMustBeLowerThanWatcherValue);
        }
    }
}
