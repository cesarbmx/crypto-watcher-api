using CryptoWatcher.Application.Requests;
using CryptoWatcher.Domain.Types;


namespace CryptoWatcher.Application.FakeRequests
{
    public static class AddIndicatorFakeRequest
    {
        public static AddIndicator GetFake_RSI()
        {
            return new AddIndicator
            {
                UserId = "cesarbmx",
                IndicatorId = "rsi",
                Name = "Relative Strength Index",
                Description = @"The Relative Strength Index (RSI) is a momentum oscillator that measures the speed and change of price movements.
                                RSI oscillates between zero and 100. Traditionally, and according to Wilder, RSI is considered overbought when above 70 and oversold when below 30.",
                Formula = "C# formula",
                Dependencies = new [] {"master.PRICE"}
            };
        }       
    }
}
