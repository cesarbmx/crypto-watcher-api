using System.Collections.Generic;
using CryptoWatcher.Application.FakeResponses;
using CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class LineChartResponseExample : IExamplesProvider<LineChart>
    {
        public LineChart GetExamples()
        {
            return FakeChart.GetFake_Bitcoin_Price();
        }
    }
    public class LineChartListResponseExample : IExamplesProvider<List<LineChart>>
    {
        public List<LineChart> GetExamples()
        {
            return FakeChart.GetFake_List();
        }
    }
}