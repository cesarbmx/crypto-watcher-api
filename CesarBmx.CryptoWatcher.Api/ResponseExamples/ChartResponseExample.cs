using System.Collections.Generic;
using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.ResponseExamples
{
    public class ChartResponseExample : IExamplesProvider<ChartResponse>
    {
        public ChartResponse GetExamples()
        {
            return FakeChart.GetFake_Bitcoin_Price();
        }
    }
    public class ChartListResponseExample : IExamplesProvider<List<ChartResponse>>
    {
        public List<ChartResponse> GetExamples()
        {
            return FakeChart.GetFake_List();
        }
    }
}