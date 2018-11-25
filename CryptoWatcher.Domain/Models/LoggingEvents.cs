

namespace CryptoWatcher.Domain.Models
{
    public enum LoggingEvents
    {
        UnhandledException,
        CurrenciesHaveBeenImported,
        ImportingCurrenciesHasFailed,
        WatchersHaveBeenUpdates,
        UpdatingWatchersHasFailed,
        WatchappsHaveBeenSent,
        SendingWatchappsHasFailed,
        OrdersHaveBeenAdded,
        AddingOrdersHasFailed
    }
}
