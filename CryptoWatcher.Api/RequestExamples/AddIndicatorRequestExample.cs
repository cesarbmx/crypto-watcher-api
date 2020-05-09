using CryptoWatcher.Application.FakeRequests;
using CryptoWatcher.Application.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.RequestExamples
{
    public class AddIndicatorRequestExample : IExamplesProvider<AddIndicatorRequest>
    {
        public AddIndicatorRequest GetExamples()
        {
            return AddIndicatorFakeRequest.GetFake_RSI();
        }
    }
}