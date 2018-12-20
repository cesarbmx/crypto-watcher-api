using CryptoWatcher.Shared.Responses;
using MediatR;

namespace CryptoWatcher.Shared.Requests
{
    public class GetVersionRequest : IRequest<VersionResponse>
    {
    }
}
