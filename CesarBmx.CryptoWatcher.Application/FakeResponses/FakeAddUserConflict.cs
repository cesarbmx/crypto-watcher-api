using CesarBmx.CryptoWatcher.Application.ConflictReasons;
using CesarBmx.CryptoWatcher.Application.Messages;
using CesarBmx.CryptoWatcher.Application.Responses;

namespace CesarBmx.CryptoWatcher.Application.FakeResponses
{
    public static class FakeAddUserConflict
    {
        public static AddUserConflict GetFake()
        {
            return new AddUserConflict(AddUserConflictReason.USER_ALREADY_EXISTS, UserMessage.UserAlreadyExists);
        }            
    }
}
