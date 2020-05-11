using System.Collections.Generic;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Types;

namespace CryptoWatcher.Application.FakeResponses
{
    public static class IndicatorFakeResponse
    {
        public static IndicatorResponse GetFake_Price()
        {
            return new IndicatorResponse
            {
                IndicatorId = "price",
                IndicatorType = IndicatorType.CurrencyIndicator,
                UserId = "master",
                Name = "Price",
                Description = "Real time price indicator",
                Formula = "C# formula"
            };
        }
        public static IndicatorResponse GetFake_RSI()
        {
            return new IndicatorResponse
            {
                IndicatorId = "rsi",
                IndicatorType = IndicatorType.CurrencyIndicator,
                UserId = "master",
                Name = "RSI",
                Description = @"The Relative Strength Index (RSI) is a momentum oscillator that measures the speed and change of price movements.
                                RSI oscillates between zero and 100. Traditionally, and according to Wilder, RSI is considered overbought when above 70 and oversold when below 30.",
                Formula = "C# formula"
            };
        }
        public static List<IndicatorResponse> GetFake_List()
        {
            return new List<IndicatorResponse>
            {
                GetFake_Price(),
                GetFake_RSI()
            };
        }
    }
}
