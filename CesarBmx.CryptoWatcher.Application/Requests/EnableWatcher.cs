using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CesarBmx.CryptoWatcher.Application.Requests
{
    public class SetWatcher
    {
        [JsonIgnore] public int  WatcherId { get; set; }
        [Required] public decimal Buy { get; set; }
        [Required] public decimal Sell { get; set; }
        [Required] public decimal Quantity { get; set; }
    }
}
