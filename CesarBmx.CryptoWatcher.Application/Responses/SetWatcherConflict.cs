using CesarBmx.CryptoWatcher.Application.ConflictReasons;
using CesarBmx.Shared.Application.Responses;


namespace CesarBmx.CryptoWatcher.Application.Responses
{
    public class SetWatcherConflict : Conflict<SetWatcherConflictReason>
    {
        public SetWatcherConflict(SetWatcherConflictReason reason, string message) : base(reason, message)
        {
        }
    }
}
