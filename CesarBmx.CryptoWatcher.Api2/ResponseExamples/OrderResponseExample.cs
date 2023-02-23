using System.Collections.Generic;
using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api2.ResponseExamples
{
    public class OrderResponseExample : IExamplesProvider<Order>
    {
        public Order GetExamples()
        {
            return FakeOrder.GetFake_Bitcoin();
        }
    }
    public class OrderListResponseExample : IExamplesProvider<List<Order>>
    {
        public List<Order> GetExamples()
        {
            return FakeOrder.GetFake_List();
        }
    }
}