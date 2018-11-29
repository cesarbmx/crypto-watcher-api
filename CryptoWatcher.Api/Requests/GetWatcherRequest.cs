using CryptoWatcher.Api.Responses;
using MediatR;

namespace CryptoWatcher.Api.Requests
{
    public class GetWatcherRequest : IRequest<WatcherResponse>
    {
        public string Id { get; set; }
    }
}
