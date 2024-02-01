using System.Collections.Generic;
using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.ResponseExamples
{
    public class LineResponseExample : IExamplesProvider<Line>
    {
        public Line GetExamples()
        {
            return FakeLine.GetFake_Bitcoin_Price();
        }
    }
    public class LineListResponseExample : IExamplesProvider<List<Line>>
    {
        public List<Line> GetExamples()
        {
            return FakeLine.GetFake_List();
        }
    }
}