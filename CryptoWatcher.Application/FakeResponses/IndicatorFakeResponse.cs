using System.Collections.Generic;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Application.FakeResponses
{
    public static class IndicatorFakeResponse
    {
        public static IndicatorResponse GetFake_PriceChange24HrsIndicator()
        {
            return new IndicatorResponse
            {
                IndicatorType = IndicatorType.CurrencyIndicator,
                IndicatorId = "johny12-price-change-24hrs",
                UserId = "johny12",
                Name = "Price Change 24 Hrs",
                Description = "It identifies immediate price changes within 24 Hrs in your portfolio",
                Formula = "C# formula"
            };
        }
        public static IndicatorResponse GetFake_HypeIndicator()
        {
            return new IndicatorResponse
            {
                IndicatorType = IndicatorType.CurrencyIndicator,
                IndicatorId = "johny12-hype",
                UserId = "johny12",
                Name = "Hype",
                Description = "It identifies immediate hypes within 24 Hrs in your portfolio",
                Formula = "C# formula"
            };
        }
        public static List<IndicatorResponse> GetFake_List()
        {
            return new List<IndicatorResponse>
            {
                GetFake_PriceChange24HrsIndicator(),
                GetFake_HypeIndicator()
            };
        }
    }
}
