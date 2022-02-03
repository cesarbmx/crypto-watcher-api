

namespace CesarBmx.CryptoWatcher.Application.Messages
{
    public static class IndicatorMessage
    {
        public const string IndicatorNotFound = "The indicator does not exist";
        public const string IndicatorWithSameIdAlreadyExists = "An indicator with the same ID already exists";
        public const string IndicatorIdHasInvalidFormat = "Only uppercase letters, hyphens and numbers are allowed";
        public const string DependenciesMustBeProvided = "Dependencies must be provided";
    }
}
