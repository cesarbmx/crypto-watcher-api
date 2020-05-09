using CryptoWatcher.Application.FakeRequests;
using CryptoWatcher.Application.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.RequestExamples
{
    public class UpdateWatcherRequestExample : IExamplesProvider<UpdateWatcherRequest>
    {
        public UpdateWatcherRequest GetExamples()
        {
            return UpdateWatcherFakeRequest.GetFake_1();
        }
    }
}