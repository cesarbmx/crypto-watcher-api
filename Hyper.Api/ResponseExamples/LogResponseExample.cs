using Hyper.Api.FakeResponses;
using Swashbuckle.AspNetCore.Filters;

namespace Hyper.Api.ResponseExamples
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