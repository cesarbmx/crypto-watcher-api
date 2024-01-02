using CesarBmx.CryptoWatcher.Application.FakeRequests;
using CesarBmx.CryptoWatcher.Application.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.RequestExamples
{
    public class UpdateIndicatorExample : IExamplesProvider<UpdateIndicatorRequest>
    {
        public UpdateIndicatorRequest GetExamples()
        {
            return FakeUpdateIndicator.GetFake_RSI();
        }
    }
}