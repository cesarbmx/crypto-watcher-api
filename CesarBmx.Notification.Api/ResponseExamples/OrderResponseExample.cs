using System.Collections.Generic;
using CesarBmx.Notification.Application.FakeResponses;
using CesarBmx.Notification.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.Notification.Api.ResponseExamples
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