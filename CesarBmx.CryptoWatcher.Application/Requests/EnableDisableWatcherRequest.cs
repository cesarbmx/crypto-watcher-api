using System.ComponentModel.DataAnnotations;
using CesarBmx.Shared.Authentication.Attributes;
using Newtonsoft.Json;

namespace CesarBmx.CryptoWatcher.Application.Requests
{
    public class EnableDisableWatcherRequest
    {
        [JsonIgnore] [Identity] public string UserId { get; set; }
        [JsonIgnore] public int  WatcherId { get; set; }
        [Required] public bool Enabled { get; set; }
    }
}
