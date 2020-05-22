using CryptoWatcher.Application.FakeRequests;
using CryptoWatcher.Application.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.RequestExamples
{
    public class UpdateIndicatorExample : IExamplesProvider<UpdateIndicator>
    {
        public UpdateIndicator GetExamples()
        {
            return UpdateIndicatorFakeRequest.GetFake_RSI();
        }
    }
}