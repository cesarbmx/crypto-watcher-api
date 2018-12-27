using CryptoWatcher.Application.Logs.FakeResponses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class LogResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return LogFakeResponse.GetFake_Log();
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