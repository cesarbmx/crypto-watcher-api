

namespace CryptoWatcher.Domain.Messages
{
    public static class IndicatorMessage
    {
        public const string IndicatorNotFound = "The indicator was not found";
        public const string IndicatorExists = "The indicator already exists";
        public const string IndicatorIdHasInvalidFormat = "Only lowercase, numbers and hyphens are allowed";
    }
}
