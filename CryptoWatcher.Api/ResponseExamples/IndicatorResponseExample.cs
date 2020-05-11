using System.Collections.Generic;
using CryptoWatcher.Application.FakeResponses;
using CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class IndicatorResponseExample : IExamplesProvider<IndicatorResponse>
    {
        public IndicatorResponse GetExamples()
        {
            return IndicatorFakeResponse.GetFake_RSI();
        }
    }
    public class IndicatorListResponseExample : IExamplesProvider<List<IndicatorResponse>>
    {
        public List<IndicatorResponse> GetExamples()
        {
            return IndicatorFakeResponse.GetFake_List();
        }
    }
}