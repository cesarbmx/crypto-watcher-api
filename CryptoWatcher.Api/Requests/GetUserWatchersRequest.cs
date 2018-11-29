using System.Collections.Generic;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Models;
using MediatR;

namespace CryptoWatcher.Api.Requests
{
    public class GetUserWatchersRequest: IRequest<List<WatcherResponse>>
    {
        public string Id { get; set; }
        public IndicatorType? IndicatorType { get; set; }
    }
}
