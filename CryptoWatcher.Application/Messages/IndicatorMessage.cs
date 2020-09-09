

namespace CryptoWatcher.Application.Messages
{
    public static class IndicatorMessage
    {
        public const string IndicatorNotFound = "The indicator does not exist";
        public const string IndicatorWithSameIdAlreadyExists = "An indicator with the same ID already exists";
        public const string IndicatorWithSameNameAlreadyExists = "An indicator with the same name already exists";
        public const string IndicatorIdHasInvalidFormat = "Only uppercase letters, hyphens and numbers are allowed";
        public const string DependenciesMustBeProvided = "Dependencies must be provided";
        public const string DependencyNotFound = "The indicator specified as dependency '{0}' does not exist";
    }
}
