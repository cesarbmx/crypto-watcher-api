using System.Collections.Generic;
using CryptoWatcher.Application.FakeResponses;
using CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class LogResponseExample : IExamplesProvider<LogResponse>
    {
        public LogResponse GetExamples()
        {
            return LogFakeResponse.GetFake_Add_Indicator();
        }
    }
    public class LogListResponseExample : IExamplesProvider<List<LogResponse>>
    {
        public List<LogResponse> GetExamples()
        {
            return LogFakeResponse.GetFake_List();
        }
    }
}