using CesarBmx.Shared.Application.Responses;
using Newtonsoft.Json;

namespace CesarBmx.CryptoWatcher.Application.Conflicts
{
    public enum AddWatcherConflictReason
    {
        WATCHER_ALREADY_EXISTS
    }

    public class AddWatcherConflict : Error
    {
        [JsonProperty(Order = 2)]
        public AddWatcherConflictReason Reason { get; set; }

        public AddWatcherConflict(AddWatcherConflictReason reason, string message)
        : base(409, message)
        {
            Reason = reason;
        }
    }
}

