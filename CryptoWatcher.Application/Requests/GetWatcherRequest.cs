using CryptoWatcher.Application.Responses;
using MediatR;

namespace CryptoWatcher.Application.Requests
{
    public class GetWatcherRequest : IRequest<WatcherResponse>
    {
        public string WatcherId { get; set; }
    }
}
