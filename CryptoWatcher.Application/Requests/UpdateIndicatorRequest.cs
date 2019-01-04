using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;


namespace CryptoWatcher.Application.Requests
{
    public class UpdateIndicatorRequest
    {
        [JsonIgnore] public string IndicatorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Formula { get; set; }
    }
}
