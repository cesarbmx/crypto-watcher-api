using CryptoWatcher.Api.FakeRequests;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.RequestExamples
{
    public class AddIndicatorRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return AddIndicatorFakeRequest.GetFake_1();
        }
    }
}