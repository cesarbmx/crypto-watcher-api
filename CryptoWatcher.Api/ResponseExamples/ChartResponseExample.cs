using System.Collections.Generic;
using CryptoWatcher.Application.FakeResponses;
using CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class LineChartResponseExample : IExamplesProvider<LineChartResponse>
    {
        public LineChartResponse GetExamples()
        {
            return LineChartFakeResponse.GetFake_Bitcoin_Price();
        }
    }
    public class LineChartListResponseExample : IExamplesProvider<List<LineChartResponse>>
    {
        public List<LineChartResponse> GetExamples()
        {
            return LineChartFakeResponse.GetFake_List();
        }
    }
}