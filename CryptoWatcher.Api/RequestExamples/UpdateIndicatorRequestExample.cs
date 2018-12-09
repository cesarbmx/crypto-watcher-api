using CryptoWatcher.Api.FakeRequests;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.RequestExamples
{
    public class UpdateIndicatorRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return UpdateIndicatorFakeRequest.GetFake_1();
        }
    }
}