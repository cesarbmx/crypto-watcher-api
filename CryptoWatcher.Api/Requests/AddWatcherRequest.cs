
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Models;
using MediatR;


namespace CryptoWatcher.Api.Requests
{
    public class AddWatcherRequest : IRequest<WatcherResponse>
    {
        public string UserId { get; set; }
        public string IndicatorId { get; set; }
        public string CurrencyId { get; set; }
        public BuySell BuySell { get; set; }
        public bool Enabled { get; set; }
    }
}
