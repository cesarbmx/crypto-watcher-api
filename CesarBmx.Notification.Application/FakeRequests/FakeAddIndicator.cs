using System.Collections.Generic;
using CesarBmx.Notification.Application.Requests;


namespace CesarBmx.Notification.Application.FakeRequests
{
    public static class FakeAddIndicator
    {
        public static AddIndicator GetFake_RSI()
        {
            return new AddIndicator
            {
                UserId = "cesarbmx",
                Abbreviation = "RSI",
                Name = "Relative Strength Index",
                Description = @"The Relative Strength Index (RSI) is a momentum oscillator that measures the speed and change of price movements.
                                RSI oscillates between zero and 100. Traditionally, and according to Wilder, RSI is considered overbought when above 70 and oversold when below 30.",
                Formula = "C# formula",
                Dependencies = new List<string> {"master.PRICE"}
            };
        }       
    }
}
