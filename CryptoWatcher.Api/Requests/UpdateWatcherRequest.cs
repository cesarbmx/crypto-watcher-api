
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Models;
using MediatR;


namespace CryptoWatcher.Api.Requests
{
    public class UpdateWatcherRequest : IRequest<WatcherResponse>
    {
        public string WatcherId { get; set; }
        public WatcherSettings Settings { get; set; }
    }
}
