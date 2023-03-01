using System.Collections.Generic;
using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.ResponseExamples
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