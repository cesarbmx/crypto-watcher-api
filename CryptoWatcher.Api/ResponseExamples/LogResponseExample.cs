using CryptoWatcher.Application.FakeResponses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class LogResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return LogFakeResponse.GetFake_Add_Indicator();
        }
    }
    public class LogListResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return LogFakeResponse.GetFake_List();
        }
    }
}