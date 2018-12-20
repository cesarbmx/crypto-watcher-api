using System;
using Newtonsoft.Json;

namespace CryptoWatcher.Application.Requests
{
    public class UpdateWatcherRequest
    {
        [JsonIgnore]
        public Guid WatcherId { get; set; }
        public decimal Buy { get; set; }
        public decimal Sell { get; set; }
        public bool Enabled { get; set; }
    }
}
