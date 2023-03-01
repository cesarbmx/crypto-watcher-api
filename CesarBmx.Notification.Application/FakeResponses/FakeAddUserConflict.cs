using CesarBmx.Notification.Application.Conflicts;
using CesarBmx.Notification.Application.Messages;

using CesarBmx.Shared.Application.Responses;
namespace CesarBmx.Notification.Application.FakeResponses
{
    public static class FakeAddUserConflict
    {
        public static Conflict<AddUserConflict> GetFake()
        {
            return new Conflict<AddUserConflict>(AddUserConflict.USER_ALREADY_EXISTS, UserMessage.UserAlreadyExists);
        }
    }
}
