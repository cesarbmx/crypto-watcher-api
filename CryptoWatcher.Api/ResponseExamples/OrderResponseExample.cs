using CryptoWatcher.Application.Orders.FakeResponses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class OrderResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return OrderFakeResponse.GetFake_bitcoin();
        }
    }
    public class OrderListResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return OrderFakeResponse.GetFake_List();
        }
    }
}