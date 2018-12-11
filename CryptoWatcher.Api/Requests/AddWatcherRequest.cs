using CryptoWatcher.Api.Responses;
using MediatR;


namespace CryptoWatcher.Api.Requests
{
    public class AddWatcherRequest : IRequest<WatcherResponse>
    {
        public string UserId { get; set; }
        public string IndicatorId { get; set; }
        public string CurrencyId { get; set; }
        public decimal Buy { get; set; }
        public decimal Sell { get; set; }
        public bool Enabled { get; set; }
    }
}
