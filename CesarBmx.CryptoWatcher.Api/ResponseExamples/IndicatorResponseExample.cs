using System.Collections.Generic;
using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.ResponseExamples
{
    public class IndicatorResponseExample : IExamplesProvider<IndicatorResponse>
    {
        public IndicatorResponse GetExamples()
        {
            return FakeIndicator.GetFake_RSI();
        }
    }
    public class IndicatorListResponseExample : IExamplesProvider<List<IndicatorResponse>>
    {
        public List<IndicatorResponse> GetExamples()
        {
            return FakeIndicator.GetFake_List();
        }
    }
}