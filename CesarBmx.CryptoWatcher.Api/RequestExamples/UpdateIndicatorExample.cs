using CesarBmx.CryptoWatcher.Application.FakeRequests;
using CesarBmx.CryptoWatcher.Application.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.RequestExamples
{
    public class UpdateIndicatorExample : IExamplesProvider<UpdateIndicator>
    {
        public UpdateIndicator GetExamples()
        {
            return FakeUpdateIndicator.GetFake_RSI();
        }
    }
}