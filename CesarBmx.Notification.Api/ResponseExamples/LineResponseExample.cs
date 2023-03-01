using System.Collections.Generic;
using CesarBmx.Notification.Application.FakeResponses;
using CesarBmx.Notification.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.Notification.Api.ResponseExamples
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