


using System;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Api.Responses
{
    public class LineResponse
    {
        public string CurrencyId { get; set; }
        public string IndicatorId { get; set; }
        public decimal Value { get; set; }
        public BuySell RecommendedBuySell { get; set; }
        public DateTime Time { get; set; }
    }
}
