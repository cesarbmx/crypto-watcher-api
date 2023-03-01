


namespace CesarBmx.CryptoWatcher.Domain.Types
{
    public enum EventType
    {
        // Currencies
        CURRENCIES_IMPORTED,

        // Indicators
        INDICATOR_ADDED,
        INDICATOR_UPDATED,
        DEPENDENCY_LEVELS_UPDATED,

        // Watchers
        WATCHER_ADDED,
        WATCHER_ENABLED,
        WATCHER_SET,
        DEFAULT_WATCHERS_UPDATED,
        WATCHERS_UPDATED,

        // Lines
        NEW_LINES_ADDED,
        OBSOLETE_LINES_REMOVED,
        
        // Orders
        ORDERS_ADDED,
        ORDERS_PROCESSED,

        // Notifications
        NOTIFICATION_ADDED,
        TELEGRAM_NOTFICATIONS_SENT,

        // Users
        USER_ADDED
    }
}
