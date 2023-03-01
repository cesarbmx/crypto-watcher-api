using CesarBmx.Notification.Application.Conflicts;
using CesarBmx.Notification.Application.Messages;

using CesarBmx.Shared.Application.Responses;
namespace CesarBmx.Notification.Application.FakeResponses
{
    public static class FakeAddWatcherConflict
    {
        public static Conflict<AddWatcherConflict> GetFake()
        {
            return new Conflict<AddWatcherConflict>(AddWatcherConflict.WATCHER_ALREADY_EXISTS, WatcherMessage.WatcherAlreadyExists);
        }
    }
}
