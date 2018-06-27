

namespace Hyper.Domain.Messages
{
    public static class AuthorizationMessages
    {
        public const string NotAuthenticated = "(#0101) You must be authenticated";
        public const string PermissionRequired = "(#0102) You don't have permission to {0}";
        public const string Restricted = "(#0103) You are restricted for this {0}: {1}";
    }
}
