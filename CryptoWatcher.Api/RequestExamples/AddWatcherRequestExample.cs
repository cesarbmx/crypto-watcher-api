using CryptoWatcher.Api.FakeRequests;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.RequestExamples
{
    public class AddWatcherRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return AddWatcherFakeRequest.GetFake_1();
        }
    }
}