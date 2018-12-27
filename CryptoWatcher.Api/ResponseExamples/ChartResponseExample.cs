using CryptoWatcher.Application.FakeResponses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class ChartResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return ChartFakeResponse.GetFake_1();
        }
    }
    public class ChartListResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return ChartFakeResponse.GetFake_List();
        }
    }
}