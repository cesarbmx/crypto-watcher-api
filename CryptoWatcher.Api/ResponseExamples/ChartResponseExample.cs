using System.Collections.Generic;
using CryptoWatcher.Application.FakeResponses;
using CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
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