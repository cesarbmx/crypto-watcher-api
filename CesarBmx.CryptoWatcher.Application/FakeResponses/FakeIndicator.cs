using System.Collections.Generic;
using CesarBmx.CryptoWatcher.Application.Responses;

namespace CesarBmx.CryptoWatcher.Application.FakeResponses
{
    public static class FakeIndicator
    {
        public static Indicator GetFake_Price()
        {
            return new Indicator
            {
                IndicatorId = "master.PRICE",
                UserId = "master",
                Abbreviation = "PRICE",
                Name = "Price",
                Description = "Real time price indicator",
                Formula = "C# formula",
                Dependencies = FakeIndicatorDependency.GetFake_List()
            };
        }
        public static Indicator GetFake_RSI()
        {
            return new Indicator
            {
                IndicatorId = "master.RSI",
                UserId = "master",
                Abbreviation = "RSI",
                Name = "RSI",
                Description = @"The Relative Strength Index (RSI) is a momentum oscillator that measures the speed and change of price movements.
                                RSI oscillates between zero and 100. Traditionally, and according to Wilder, RSI is considered overbought when above 70 and oversold when below 30.",
                Formula = "C# formula",
                Dependencies = FakeIndicatorDependency.GetFake_List()
            };
        }
        public static List<Indicator> GetFake_List()
        {
            return new List<Indicator>
            {
                GetFake_Price(),
                GetFake_RSI()
            };
        }
    }
}
