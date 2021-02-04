using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;


namespace CryptoWatcher.Application.Requests
{
    public class AddWatcher 
    {
        [JsonIgnore] public string UserId { get; set; }
        [Required] public string CurrencyId { get; set; }
        [Required] public string CreatorId { get; set; }
        [Required] public string IndicatorId { get; set; }
        [Required] public bool Enabled { get; set; }
    }
}
