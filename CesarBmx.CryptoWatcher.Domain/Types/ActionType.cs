using System;


namespace CesarBmx.CryptoWatcher.Domain.Types
{
    [Flags]
    public enum ActionType
    {
        // User
        ADD_USER,

        // Watcher
        ADD_WARTCHER,
        SET_WARTCHER,
        ENABLE_WATCHER,

        // Indicator
        ADD_INDICATOR,
        UPDATE_INDICATOR,
    }
}
