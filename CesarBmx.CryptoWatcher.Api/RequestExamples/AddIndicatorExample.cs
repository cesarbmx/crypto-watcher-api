using CesarBmx.CryptoWatcher.Application.FakeRequests;
using CesarBmx.CryptoWatcher.Application.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.RequestExamples
{
    public class AddIndicatorExample : IExamplesProvider<AddIndicator>
    {
        public AddIndicator GetExamples()
        {
            return FakeAddIndicator.GetFake_RSI();
        }
    }
}