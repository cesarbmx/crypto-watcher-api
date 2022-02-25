using CesarBmx.CryptoWatcher.Application.ConflictReasons;
using CesarBmx.Shared.Application.Responses;


namespace CesarBmx.CryptoWatcher.Application.Responses
{
    public class AddWatcherConflict : Conflict<AddWatcherConflictReason>
    {
        public AddWatcherConflict(AddWatcherConflictReason reason, string message) : base(reason, message)
        {
        }
    }
}
