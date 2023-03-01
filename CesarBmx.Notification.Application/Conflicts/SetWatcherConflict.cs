

namespace CesarBmx.Notification.Application.Conflicts
{
    public enum SetWatcherConflict
    {
        BUY_LIMIT_MUST_BE_LOWER_THAN_WATCHER_VALUE,
        SELL_LIMIT_MUST_BE_HIGHER_THAN_WATCHER_VALUE,
        WATCHER_ALREADY_BOUGHT,
        WATCHER_ALREADY_SOLD,
    }
}
