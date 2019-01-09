using CryptoWatcher.Application.Requests;

namespace CryptoWatcher.Application.FakeRequests
{
    public static class UpdateIndicatorFakeRequest
    {
        public static UpdateIndicatorRequest GetFake_1()
        {
            return new UpdateIndicatorRequest
            {
                IndicatorId = "hype",
                Name = "Hype",
                Description = "It identifies immediate hypes within 24 Hrs in your portfolio",
                Formula = "C# formula",
                Dependencies = new[] {"price", "price-change-24hrs" }
            };
        }       
    }
}
