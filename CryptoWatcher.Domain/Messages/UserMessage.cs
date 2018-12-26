

namespace CryptoWatcher.Domain.Messages
{
    public static class UserMessage
    {
        public const string UserNotFound = "The user was not found";
        public const string UserExists = "The user already exists";
        public const string UserIdCannotBeEmpty = "The id cannot be empty";
    }
}
