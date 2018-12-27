using System;
using CryptoWatcher.Application.Watchers.Responses;
using MediatR;


namespace CryptoWatcher.Application.Watchers.Requests
{
    public class GetWatcherRequest : IRequest<WatcherResponse>
    {
        public Guid WatcherId { get; set; }
    }
}
