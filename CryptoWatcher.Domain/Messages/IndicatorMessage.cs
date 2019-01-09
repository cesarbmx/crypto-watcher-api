

namespace CryptoWatcher.Domain.Messages
{
    public static class IndicatorMessage
    {
        public const string IndicatorNotFound = "The indicator was not found";
        public const string IndicatorExists = "The indicator already exists";
        public const string IndicatorIdHasInvalidFormat = "Only lowercase, numbers and hyphens are allowed";
        public const string DepenedenciesMustBeProvided = "Depenedencies must be provided. Consider passing an empty array in case the indicator has no dependencies.";
    }
}
