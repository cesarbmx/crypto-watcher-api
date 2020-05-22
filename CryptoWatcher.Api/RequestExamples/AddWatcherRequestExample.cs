using CryptoWatcher.Application.FakeRequests;
using CryptoWatcher.Application.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.RequestExamples
{
    public class AddWatcherRequestExample : IExamplesProvider<AddWatcher>
    {
        public AddWatcher GetExamples()
        {
            return AddWatcherFakeRequest.GetFake_RSI();
        }
    }
}