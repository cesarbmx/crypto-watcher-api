using CesarBmx.Notification.Application.Conflicts;
using CesarBmx.Notification.Application.Messages;
using CesarBmx.Shared.Application.Responses;

namespace CesarBmx.Notification.Application.FakeResponses
{
    public static class FakeSetWatcherConflict
    {
        public static Conflict<SetWatcherConflict> GetFake()
        {
            return new Conflict<SetWatcherConflict>(SetWatcherConflict.BUY_LIMIT_MUST_BE_LOWER_THAN_WATCHER_VALUE, WatcherMessage.BuyLimitMustBeLowerThanWatcherValue);
        }
    }
}
