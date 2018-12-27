using System.ComponentModel.DataAnnotations;
using CryptoWatcher.Application.Watchers.Responses;
using MediatR;

namespace CryptoWatcher.Application.Watchers.Requests
{
    public class AddWatcherRequest : IRequest<WatcherResponse>
    {
        [Required] public string UserId { get; set; }
        [Required] public string IndicatorId { get; set; }
        [Required] public string CurrencyId { get; set; }
        public decimal? Buy { get; set; }
        public decimal? Sell { get; set; }
        [Required] public bool Enabled { get; set; }
    }
}
