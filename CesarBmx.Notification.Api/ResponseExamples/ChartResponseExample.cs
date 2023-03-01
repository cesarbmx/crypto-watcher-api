using System.Collections.Generic;
using CesarBmx.Notification.Application.FakeResponses;
using CesarBmx.Notification.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.Notification.Api.ResponseExamples
{
    public class ChartResponseExample : IExamplesProvider<Chart>
    {
        public Chart GetExamples()
        {
            return FakeChart.GetFake_Bitcoin_Price();
        }
    }
    public class ChartListResponseExample : IExamplesProvider<List<Chart>>
    {
        public List<Chart> GetExamples()
        {
            return FakeChart.GetFake_List();
        }
    }
}