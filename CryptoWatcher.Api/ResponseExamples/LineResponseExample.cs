using System.Collections.Generic;
using CryptoWatcher.Application.FakeResponses;
using CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
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