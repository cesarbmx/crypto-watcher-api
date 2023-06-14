using CesarBmx.Shared.Application.Responses;
using Newtonsoft.Json;

namespace CesarBmx.CryptoWatcher.Application.Conflicts
{
    public enum UpdateIndicatorConflictReason
    {
    }

    public class UpdateIndicatorConflict : Error
    {
        [JsonProperty(Order = 2)]
        public UpdateIndicatorConflictReason Reason { get; set; }

        public UpdateIndicatorConflict(UpdateIndicatorConflictReason reason, string message)
        : base(409, message)
        {
            Reason = reason;
        }
    }
}
