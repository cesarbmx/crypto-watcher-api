using CryptoWatcher.Application.FakeRequests;
using CryptoWatcher.Application.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.RequestExamples
{
    public class UpdateIndicatorRequestExample : IExamplesProvider<UpdateIndicatorRequest>
    {
        public UpdateIndicatorRequest GetExamples()
        {
            return UpdateIndicatorFakeRequest.GetFake_RSI();
        }
    }
}