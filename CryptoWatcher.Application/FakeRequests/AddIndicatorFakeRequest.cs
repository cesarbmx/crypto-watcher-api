using CryptoWatcher.Application.Requests;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Application.FakeRequests
{
    public static class AddIndicatorFakeRequest
    {
        public static AddIndicatorRequest GetFake_1()
        {
            return new AddIndicatorRequest
            {
                IndicatorId = "hype",
                UserId = "johny.melavo",
                IndicatorType = IndicatorType.CurrencyIndicator,
                Name = "Hype",
                Description = "It identifies immediate hypes within 24 Hrs in your portfolio",
                Formula = "C# formula",
                Dependencies = "\"price-change=24hrs\" "
            };
        }       
    }
}
