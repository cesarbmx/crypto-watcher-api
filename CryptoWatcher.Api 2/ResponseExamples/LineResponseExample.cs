using CryptoWatcher.Application.FakeResponses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class LineResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return LineFakeResponse.GetFake_Bitcoin_Price();
        }
    }
    public class LineListResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return LineFakeResponse.GetFake_List();
        }
    }
}