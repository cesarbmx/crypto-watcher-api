
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Models;
using MediatR;
using Newtonsoft.Json;


namespace CryptoWatcher.Api.Requests
{
    public class UpdateWatcherRequest : IRequest<WatcherResponse>
    {
        [JsonIgnore]
        public string WatcherId { get; set; }
        public BuySell BuySell { get; set; }
        public bool Enabled { get; set; }
    }
}
