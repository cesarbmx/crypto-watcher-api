using CesarBmx.CryptoWatcher.Application.ConflictReasons;
using CesarBmx.CryptoWatcher.Application.Messages;

using CesarBmx.Shared.Application.Responses;
namespace CesarBmx.CryptoWatcher.Application.FakeResponses
{
    public static class FakeAddUserConflict
    {
        public static Conflict<AddUserConflictReason> GetFake()
        {
            return new Conflict<AddUserConflictReason>(AddUserConflictReason.USER_ALREADY_EXISTS, UserMessage.UserAlreadyExists);
        }
    }
}
