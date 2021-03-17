using CesarBmx.CryptoWatcher.Application.Requests;

namespace CesarBmx.CryptoWatcher.Application.FakeRequests
{
    public static class UpdateIndicatorFakeRequest
    {
        public static UpdateIndicator GetFake_RSI()
        {
            return new UpdateIndicator
            {
                UserId = "cesarbmx",
                IndicatorId = "rsi",
                Name = "Relative Strength Index",
                Description = @"The Relative Strength Index (RSI) is a momentum oscillator that measures the speed and change of price movements.
                                RSI oscillates between zero and 100. Traditionally, and according to Wilder, RSI is considered overbought when above 70 and oversold when below 30.",
                Formula = "C# formula",
                Dependencies = new[] {"master.price", "master.price-change-24hrs" }
            };
        }       
    }
}
