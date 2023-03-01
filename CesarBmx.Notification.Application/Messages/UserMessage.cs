

namespace CesarBmx.Notification.Application.Messages
{
    public static class UserMessage
    {
        public const string UserNotFound = "The user does not exist";
        public const string UserAlreadyExists = "The user already exists";
        public const string UserIdMustBeLowerCase = "The user ID must be lower case";
        public const string UserIdMustContainOnlyLettersOrNumbers = "The user ID must contain only letters or numbers";
        public const string UserIdMustContainAtLeastOneLetter = "The user ID must contain, at least, one letter";
    }
}
