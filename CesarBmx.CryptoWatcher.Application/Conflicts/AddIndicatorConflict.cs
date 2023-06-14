using CesarBmx.Shared.Application.Responses;
using Newtonsoft.Json;

namespace CesarBmx.CryptoWatcher.Application.Conflicts
{
    public enum AddIndicatorConflictReason
    {
        INDICATOR_ALREADY_EXISTS
    }

    public class AddIndicatorConflict : Error
    {
        [JsonProperty(Order = 2)]
        public AddIndicatorConflictReason Reason { get; set; }

        public AddIndicatorConflict(AddIndicatorConflictReason reason, string message)
        : base(409, message)
        {
            Reason = reason;
        }
    }
}
