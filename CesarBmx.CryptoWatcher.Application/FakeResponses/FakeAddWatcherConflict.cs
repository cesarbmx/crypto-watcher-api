using CesarBmx.CryptoWatcher.Application.Conflicts;
using CesarBmx.CryptoWatcher.Application.Messages;

using CesarBmx.Shared.Application.Responses;
namespace CesarBmx.CryptoWatcher.Application.FakeResponses
{
    public static class FakeAddWatcherConflict
    {
        public static AddWatcherConflict GetFake()
        {
            return new AddWatcherConflict(AddWatcherConflictReason.WATCHER_ALREADY_EXISTS, WatcherMessage.WatcherAlreadyExists);
        }
    }
}
