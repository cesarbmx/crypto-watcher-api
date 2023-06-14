using CesarBmx.Shared.Application.Responses;
using Newtonsoft.Json;

namespace CesarBmx.CryptoWatcher.Application.Conflicts
{
    public enum EnableWatcherConflictReason
    {
        WATCHER_ALREADY_ENABLED,
        WATCHER_ALREADY_DISABLED,
    }

    public class EnableWatcherConflict : Error
    {
        [JsonProperty(Order = 2)]
        public EnableWatcherConflictReason Reason { get; set; }

        public EnableWatcherConflict(EnableWatcherConflictReason reason, string message)
        : base(409, message)
        {
            Reason = reason;
        }
    }
}

