

using Newtonsoft.Json;

namespace Hyper.Api.Responses
{
    public class CurrencyResponse
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public short Rank { get; set; }
        public decimal Price { get; set; }
        public decimal MarketCap { get; set; }
        [JsonProperty(PropertyName = "volume_24h")]
        public decimal Volume24H { get; set; }
        [JsonProperty(PropertyName = "percentage_change_24h")]
        public decimal PercentageChange24H { get; set; }
    }
}
