using CesarBmx.Shared.Authentication.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace CesarBmx.CryptoWatcher.Application.Requests
{
    public class AddWatcherRequest 
    {
        [JsonIgnore] [Identity] public string UserId { get; set; }
        [Required] public string CurrencyId { get; set; }
        [Required] public string IndicatorId { get; set; }
        [Required] public bool Enabled { get; set; }
    }
}
