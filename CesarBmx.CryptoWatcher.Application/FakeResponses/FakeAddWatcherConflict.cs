using CesarBmx.CryptoWatcher.Application.Conflicts;
using CesarBmx.CryptoWatcher.Application.Messages;

using CesarBmx.Shared.Application.Responses;
namespace CesarBmx.CryptoWatcher.Application.FakeResponses
{
    public static class FakeAddWatcherConflict
    {
        public static Conflict<AddWatcherConflict> GetFake()
        {
            return new Conflict<AddWatcherConflict>(AddWatcherConflict.WATCHER_ALREADY_EXISTS, WatcherMessage.WatcherAlreadyExists);
        }
    }
}
