

namespace CryptoWatcher.Domain.Models
{
    public enum LoggingEvents
    {
        ExceptionUnhandled,
        CurrenciesImported,
        ImportingCurrenciesFailed,
        WatchappsSent,
        SendingWatchappsFailed,
        OrdersAdded,
        AddingOrdersFailed,
        WatcherAdded
    }
}
