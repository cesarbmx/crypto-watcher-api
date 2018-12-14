using CryptoWatcher.Application.FakeResponses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class IndicatorResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return IndicatorFakeResponse.GetFake_HypeIndicator();
        }
    }
    public class IndicatorListResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return IndicatorFakeResponse.GetFake_List();
        }
    }
}