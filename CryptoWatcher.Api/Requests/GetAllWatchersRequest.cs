using System.Collections.Generic;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Models;
using MediatR;

namespace CryptoWatcher.Api.Requests
{
    public class GetAllWatchersRequest: IRequest<List<WatcherResponse>>
    {
        public string UserId { get; set; }
        public IndicatorType? IndicatorType { get; set; }
    }
}
