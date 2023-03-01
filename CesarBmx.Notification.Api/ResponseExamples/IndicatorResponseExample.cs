using System.Collections.Generic;
using CesarBmx.Notification.Application.FakeResponses;
using CesarBmx.Notification.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.Notification.Api.ResponseExamples
{
    public class IndicatorResponseExample : IExamplesProvider<Indicator>
    {
        public Indicator GetExamples()
        {
            return FakeIndicator.GetFake_RSI();
        }
    }
    public class IndicatorListResponseExample : IExamplesProvider<List<Indicator>>
    {
        public List<Indicator> GetExamples()
        {
            return FakeIndicator.GetFake_List();
        }
    }
}