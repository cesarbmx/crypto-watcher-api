

namespace CryptoWatcher.Domain.Models
{
    public enum LoggingEvents
    {
        UnhandledException,
        CurrenciesHaveBeenImported,
        ImportingCurrenciesHasFailed,
        WatchersHaveBeenSet,
        UpdatingWatchersHasFailed,
        WatchappsHaveBeenSent,
        SendingWatchappsHasFailed
    }
}
