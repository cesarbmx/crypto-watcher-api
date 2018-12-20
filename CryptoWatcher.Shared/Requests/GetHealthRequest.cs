using CryptoWatcher.Shared.Responses;
using MediatR;

namespace CryptoWatcher.Shared.Requests
{
    public class GetHealthRequest : IRequest<HealthResponse>
    {
    }
}
