using CesarBmx.Shared.Application.Responses;
using Newtonsoft.Json;

namespace CesarBmx.CryptoWatcher.Application.Conflicts
{
    public enum SetWatcherError
    {
        BUY_LIMIT_MUST_BE_LOWER_THAN_WATCHER_VALUE,
        SELL_LIMIT_MUST_BE_HIGHER_THAN_WATCHER_VALUE,
        WATCHER_ALREADY_BOUGHT,
        WATCHER_ALREADY_SOLD
    }
}
