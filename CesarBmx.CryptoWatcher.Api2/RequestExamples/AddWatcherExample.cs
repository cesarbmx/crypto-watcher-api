using CesarBmx.CryptoWatcher.Application.FakeRequests;
using CesarBmx.CryptoWatcher.Application.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api2.RequestExamples
{
    public class AddWatcherExample : IExamplesProvider<AddWatcher>
    {
        public AddWatcher GetExamples()
        {
            return FakeAddWatcher.GetFake_RSI();
        }
    }
}