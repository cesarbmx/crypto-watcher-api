using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CesarBmx.CryptoWatcher.Application.Requests
{
    public class EnableWatcher
    {
        [JsonIgnore] public int  WatcherId { get; set; }
        [Required] public bool Enabled { get; set; }
    }
}
