using System.Collections.Generic;
using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.CryptoWatcher.Application.Resources;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.ResponseExamples
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