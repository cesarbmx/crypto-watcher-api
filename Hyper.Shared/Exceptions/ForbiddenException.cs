

namespace CryptoWatcher.Shared.Exceptions
{
    public class ForbiddenException : DomainException
    {
        public ForbiddenException(string message) : base(message)
        {}
    }
}
