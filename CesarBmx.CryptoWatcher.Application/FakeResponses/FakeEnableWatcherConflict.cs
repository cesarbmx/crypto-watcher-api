using CesarBmx.CryptoWatcher.Application.ConflictReasons;
using CesarBmx.CryptoWatcher.Application.Messages;
using CesarBmx.Shared.Application.Responses;

namespace CesarBmx.CryptoWatcher.Application.FakeResponses
{
    public static class FakeEnableWatcherConflict
    {
        public static Conflict<EnableWatcherConflictReason> GetFake()
        {
            return new Conflict<EnableWatcherConflictReason>(EnableWatcherConflictReason.WATCHER_ALREADY_ENABLED, WatcherMessage.WatcherAlreadyEnabled);
        }
    }
}
