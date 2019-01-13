

namespace CryptoWatcher.Application.Messages
{
    public static class IndicatorMessage
    {
        public const string IndicatorNotFound = "The indicator was not found";
        public const string IndicatorAlreadyExists = "The indicator already exists";
        public const string IndicatorIdHasInvalidFormat = "Only lowercase, numbers and hyphens are allowed";
        public const string DepenedenciesMustBeProvided = "Depenedencies must be provided";
        public const string DepenedencyNotFound = "The indicator specified as dependency '{0}' does not exist";
    }
}
