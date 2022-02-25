using CesarBmx.CryptoWatcher.Application.ConflictReasons;
using CesarBmx.CryptoWatcher.Application.Messages;
using CesarBmx.CryptoWatcher.Application.Responses;

namespace CesarBmx.CryptoWatcher.Application.FakeResponses
{
    public static class FakeAddWatcherConflict
    {
        public static AddWatcherConflict GetFake()
        {
            return new AddWatcherConflict(AddWatcherConflictReason.WATCHER_ALREADY_EXISTS, WatcherMessage.WatcherAlreadyLiquidated);
        }
    }
}
