

namespace CryptoWatcher.Domain.Messages
{
    public static class Message
    {
        // Authentication
        public const string NotAuthenticated = "You must be authenticated";
        public const string PermissionRequired = "You don't have permission to {0}";
        public const string Restricted = "You are restricted for this {0}: {1}";

        // Validation
        public const string BadRequest = "The request is invalid";
        public const string ValidationFailed = "Validation failed";
        public const string NotFound = "The resource was not found";
        public const string Conflict = "There is a conflict with the current state";
        public const string InternalServerError = "Internal Server Error";
       
    }
}