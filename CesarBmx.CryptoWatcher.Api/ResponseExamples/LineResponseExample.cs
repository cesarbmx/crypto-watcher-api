using System.Collections.Generic;
using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.ResponseExamples
{
    public class LineResponseExample : IExamplesProvider<LineResponse>
    {
        public LineResponse GetExamples()
        {
            return FakeLine.GetFake_Bitcoin_Price();
        }
    }
    public class LineListResponseExample : IExamplesProvider<List<LineResponse>>
    {
        public List<LineResponse> GetExamples()
        {
            return FakeLine.GetFake_List();
        }
    }
}