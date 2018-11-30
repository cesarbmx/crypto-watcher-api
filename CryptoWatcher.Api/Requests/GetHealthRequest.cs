using CryptoWatcher.Api.Responses;
using MediatR;

namespace CryptoWatcher.Api.Requests
{
    public class GetHealthRequest : IRequest<HealthResponse>
    {
    }
}
