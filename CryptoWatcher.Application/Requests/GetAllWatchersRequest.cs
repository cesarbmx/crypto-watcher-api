using System.Collections.Generic;
using CryptoWatcher.Application.Responses;
using MediatR;

namespace CryptoWatcher.Application.Requests
{
    public class GetAllWatchersRequest: IRequest<List<WatcherResponse>>
    {
        public string UserId { get; set; }
        public string IndicatorId { get; set; }
    }
}
