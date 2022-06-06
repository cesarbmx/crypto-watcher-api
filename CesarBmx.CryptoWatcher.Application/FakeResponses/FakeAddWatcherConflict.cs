using CesarBmx.CryptoWatcher.Application.ConflictReasons;
using CesarBmx.CryptoWatcher.Application.Messages;

using CesarBmx.Shared.Application.Responses;
namespace CesarBmx.CryptoWatcher.Application.FakeResponses
{
    public static class FakeAddWatcherConflict
    {
        public static Conflict<AddWatcherConflictReason> GetFake()
        {
            return new Conflict<AddWatcherConflictReason>(AddWatcherConflictReason.WATCHER_ALREADY_EXISTS, WatcherMessage.WatcherAlreadyExists);
        }
    }
}
