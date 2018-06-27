

namespace Hyper.Shared.Exceptions
{
    public class UnauthorizedException : DomainException
    {
        public UnauthorizedException(string message) : base(message)
        {}
    }
}
