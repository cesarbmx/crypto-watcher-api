using CesarBmx.Notification.Application.Conflicts;
using CesarBmx.Notification.Application.Messages;
using CesarBmx.Shared.Application.Responses;

namespace CesarBmx.Notification.Application.FakeResponses
{
    public static class FakeEnableWatcherConflict
    {
        public static Conflict<EnableWatcherConflict> GetFake()
        {
            return new Conflict<EnableWatcherConflict>(EnableWatcherConflict.WATCHER_ALREADY_ENABLED, WatcherMessage.WatcherAlreadyEnabled);
        }
    }
}
