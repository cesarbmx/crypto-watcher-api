using CesarBmx.CryptoWatcher.Application.Conflicts;
using CesarBmx.CryptoWatcher.Application.Messages;

using CesarBmx.Shared.Application.Responses;
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
