using CesarBmx.CryptoWatcher.Application.ConflictReasons;
using CesarBmx.Shared.Application.Responses;


namespace CesarBmx.CryptoWatcher.Application.Responses
{
    public class AddIndicatorConflict : Conflict<AddIndicatorConflictReason>
    {
        public AddIndicatorConflict(AddIndicatorConflictReason reason, string message) : base(reason, message)
        {
        }
    }
}
