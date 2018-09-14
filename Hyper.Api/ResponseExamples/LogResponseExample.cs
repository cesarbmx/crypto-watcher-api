using Hyper.Api.FakeResponses;
using Swashbuckle.AspNetCore.Examples;

namespace Hyper.Api.ResponseExamples
{
    public class LogResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return LogFakeResponse.GetFake_Error();
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