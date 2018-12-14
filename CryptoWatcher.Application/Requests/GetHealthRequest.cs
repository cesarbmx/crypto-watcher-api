using CryptoWatcher.Application.Responses;
using MediatR;

namespace CryptoWatcher.Application.Requests
{
    public class GetHealthRequest : IRequest<HealthResponse>
    {
    }
}
