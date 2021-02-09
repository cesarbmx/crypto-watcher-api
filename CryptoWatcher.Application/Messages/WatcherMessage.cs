

namespace CryptoWatcher.Application.Messages
{
    public static class WatcherMessage
    {
        public const string WatcherNotFound = "The watcher does not exist";
        public const string WatcherAlreadyExists = "The watcher already exists";
        public const string WatcherDoesNotBelongToYou = "The watcher does not belong to you. Therefore, it can´t be updated";
    }
}
