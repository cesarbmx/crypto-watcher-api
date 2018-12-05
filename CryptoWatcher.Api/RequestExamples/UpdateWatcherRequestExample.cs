using CryptoWatcher.Api.FakeRequests;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.RequestExamples
{
    public class UpdateWatcherRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return UpdateWatcherFakeRequest.GetFake_1();
        }
    }
}