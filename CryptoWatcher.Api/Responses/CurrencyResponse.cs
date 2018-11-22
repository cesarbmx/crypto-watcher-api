

using Newtonsoft.Json;

namespace CryptoWatcher.Api.Responses
{
    public class CurrencyResponse
    {
        public string CurrencyId { get; set; }
        public string CurrencySymbol { get; set; }
        public string CurrencyName { get; set; }
        public short CurrencyRank { get; set; }
        public decimal CurrencyPrice { get; set; }
        public decimal CurrencyMarketCap { get; set; }
        [JsonProperty(PropertyName = "currency_volume_24h")]
        public decimal CurrencyVolume24H { get; set; }
        [JsonProperty(PropertyName = "currency_percentage_change_24h")]
        public decimal CurrencyPercentageChange24H { get; set; }
    }
}
