using CryptoWatcher.Application.FakeResponses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class LineChartResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return LineChartFakeResponse.GetFake_1();
        }
    }
    public class LineChartListResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return LineChartFakeResponse.GetFake_List();
        }
    }
}