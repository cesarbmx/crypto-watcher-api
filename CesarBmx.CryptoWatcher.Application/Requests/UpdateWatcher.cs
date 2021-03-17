using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CesarBmx.CryptoWatcher.Application.Requests
{
    public class UpdateWatcher
    {
        [JsonIgnore] public int  WatcherId { get; set; }
        [Required] public decimal Buy { get; set; }
        [Required] public decimal Sell { get; set; }
        [Required] public bool Enabled { get; set; }
    }
}
