using CesarBmx.CryptoWatcher.Application.ConflictReasons;
using CesarBmx.Shared.Application.Responses;


namespace CesarBmx.CryptoWatcher.Application.Responses
{
    public class AddUserConflict : Conflict<AddUserConflictReason>
    {
        public AddUserConflict(AddUserConflictReason reason, string message) : base(reason, message)
        {
        }
    }
}
