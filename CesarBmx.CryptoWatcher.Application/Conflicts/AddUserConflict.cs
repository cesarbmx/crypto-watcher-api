using CesarBmx.Shared.Application.Responses;
using Newtonsoft.Json;

namespace CesarBmx.CryptoWatcher.Application.Conflicts
{
    public enum AddUserConflictReason
    {
        USER_ALREADY_EXISTS
    }

    public class AddUserConflict : Error
    {
        [JsonProperty(Order = 2)]
        public AddUserConflictReason Reason { get; set; }

        public AddUserConflict(AddUserConflictReason reason, string message)
        : base(409, message)
        {
            Reason = reason;
        }
    }
}

