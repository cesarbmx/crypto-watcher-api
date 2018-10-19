using System;
using System.Collections.Generic;
using Hyper.Api.Responses;

namespace Hyper.Api.FakeResponses
{
    public static class LogFakeResponse
    {
        public static LogResponse GetFake_Log()
        {
            return new LogResponse
            {              
                Id = Guid.Parse("2f0bd0cd-6b95-4759-afbc-d25c570d823c"),
                Message = "AllCurrenciesImported",
                CreationTime = DateTime.Parse("2018-09-14T14:05")
            };
        }
        public static IEnumerable<LogResponse> GetFake_List()
        {
            return new List<LogResponse>
            {
                GetFake_Log()
            };
        }
    }
}
