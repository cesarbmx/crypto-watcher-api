using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CryptoWatcher.Application.Requests
{
    public class UpdateIndicator
    {
        [JsonIgnore] public string IndicatorId { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
        [Required] public string Formula { get; set; }
        [Required] public string[] Dependencies { get; set; }
    }
}
