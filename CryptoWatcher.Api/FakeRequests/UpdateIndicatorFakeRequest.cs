using CryptoWatcher.Api.Requests;

namespace CryptoWatcher.Api.FakeRequests
{
    public static class UpdateIndicatorFakeRequest
    {
        public static UpdateIndicatorRequest GetFake_1()
        {
            return new UpdateIndicatorRequest
            {
                IndicatorId = "cesarbmx-hype",
                Name = "Hype",
                Description = "It identifies inmediate hypes within 24 Hrs in your portfolio",
                Formula = "C# formula"
            };
        }       
    }
}
