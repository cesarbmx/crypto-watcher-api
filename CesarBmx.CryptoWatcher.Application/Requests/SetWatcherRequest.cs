using System.ComponentModel.DataAnnotations;
using CesarBmx.Shared.Authentication.Attributes;
using Newtonsoft.Json;

namespace CesarBmx.CryptoWatcher.Application.Requests
{
    public class SetWatcherRequest
    {
        [JsonIgnore] [Identity] public string UserId { get; set; }
        [JsonIgnore] public int  WatcherId { get; set; }
        [Required] public decimal Buy { get; set; }
        public decimal? Sell { get; set; }
        [Required] public decimal Quantity { get; set; }
    }
}
