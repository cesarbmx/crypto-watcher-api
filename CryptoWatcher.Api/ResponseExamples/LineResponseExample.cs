using System.Collections.Generic;
using CryptoWatcher.Application.FakeResponses;
using CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class LineResponseExample : IExamplesProvider<LineResponse>
    {
        public LineResponse GetExamples()
        {
            return LineFakeResponse.GetFake_Bitcoin_Price();
        }
    }
    public class LineListResponseExample : IExamplesProvider<List<LineResponse>>
    {
        public List<LineResponse> GetExamples()
        {
            return LineFakeResponse.GetFake_List();
        }
    }
}