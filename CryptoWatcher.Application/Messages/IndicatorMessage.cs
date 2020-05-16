

namespace CryptoWatcher.Application.Messages
{
    public static class IndicatorMessage
    {
        public const string IndicatorNotFound = "The indicator does not exist";
        public const string IndicatorWithSameIdAlreadyExists = "An indicator with the same ID already exists";
        public const string IndicatorWithSameNameAlreadyExists = "An indicator with the same name already exists";
        public const string IndicatorIdHasInvalidFormat = "Only lowercase, numbers and hyphens are allowed";
        public const string DepenedenciesMustBeProvided = "Depenedencies must be provided";
        public const string DepenedencyNotFound = "The indicator specified as dependency '{0}' does not exist";
    }
}
