using System.Collections.Generic;
using CryptoWatcher.Application.Watchers.Responses;
using MediatR;

namespace CryptoWatcher.Application.Watchers.Requests
{
    public class GetAllWatchersRequest: IRequest<List<WatcherResponse>>
    {
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public string IndicatorId { get; set; }
    }
}
