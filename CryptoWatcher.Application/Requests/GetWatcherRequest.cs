using System;
using CryptoWatcher.Application.Responses;
using MediatR;

namespace CryptoWatcher.Application.Requests
{
    public class GetWatcherRequest : IRequest<WatcherResponse>
    {
        public Guid WatcherId { get; set; }
    }
}
