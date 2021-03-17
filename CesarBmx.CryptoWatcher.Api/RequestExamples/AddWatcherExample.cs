using CesarBmx.CryptoWatcher.Application.FakeRequests;
using CesarBmx.CryptoWatcher.Application.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.RequestExamples
{
    public class AddWatcherExample : IExamplesProvider<AddWatcher>
    {
        public AddWatcher GetExamples()
        {
            return AddWatcherFakeRequest.GetFake_RSI();
        }
    }
}