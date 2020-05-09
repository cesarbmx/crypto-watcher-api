using System.Collections.Generic;
using CryptoWatcher.Application.FakeResponses;
using CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class OrderResponseExample : IExamplesProvider<OrderResponse>
    {
        public OrderResponse GetExamples()
        {
            return OrderFakeResponse.GetFake_Bitcoin();
        }
    }
    public class OrderListResponseExample : IExamplesProvider<List<OrderResponse>>
    {
        public List<OrderResponse> GetExamples()
        {
            return OrderFakeResponse.GetFake_List();
        }
    }
}