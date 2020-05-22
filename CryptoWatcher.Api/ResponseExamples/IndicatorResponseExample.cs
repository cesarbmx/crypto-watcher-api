using System.Collections.Generic;
using CryptoWatcher.Application.FakeResponses;
using CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class IndicatorResponseExample : IExamplesProvider<Indicator>
    {
        public Indicator GetExamples()
        {
            return FakeIndicator.GetFake_RSI();
        }
    }
    public class IndicatorListResponseExample : IExamplesProvider<List<Indicator>>
    {
        public List<Indicator> GetExamples()
        {
            return FakeIndicator.GetFake_List();
        }
    }
}