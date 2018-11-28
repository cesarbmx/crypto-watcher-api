

namespace CryptoWatcher.Domain.Models
{
    public enum LoggingEvents
    {
        ExceptionUnhandled,
        CurrenciesImported,
        ImportingCurrenciesFailed,
        WatchappsSent,
        SendingWatchappFailed,
        OrdersAdded,
        AddingOrdersFailed,
        WatcherAdded,
        ConnectingToTwilioFailed
    }
}
