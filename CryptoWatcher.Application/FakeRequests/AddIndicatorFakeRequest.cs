using CryptoWatcher.Application.Requests;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Application.FakeRequests
{
    public static class AddIndicatorFakeRequest
    {
        public static AddIndicatorRequest GetFake_RSI()
        {
            return new AddIndicatorRequest
            {
                IndicatorId = "RSI",
                UserId = "master",
                IndicatorType = IndicatorType.CurrencyIndicator,
                Name = "Relative Strength Index",
                Description = @"The Relative Strength Index (RSI) is a momentum oscillator that measures the speed and change of price movements.
                                RSI oscillates between zero and 100. Traditionally, and according to Wilder, RSI is considered overbought when above 70 and oversold when below 30.",
                Formula = "C# formula",
                Dependencies = new [] {"price"}
            };
        }       
    }
}
