using CryptoWatcher.Api.Responses;
using MediatR;
using Newtonsoft.Json;

namespace CryptoWatcher.Api.Requests
{
    public class UpdateWatcherRequest : IRequest<WatcherResponse>
    {
        [JsonIgnore]
        public string WatcherId { get; set; }
        public decimal Buy { get; set; }
        public decimal Sell { get; set; }
        public bool Enabled { get; set; }
    }
}
