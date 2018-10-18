using System;
using System.Collections.Generic;
using Hyper.Api.Responses;
using Hyper.Domain.Models;

namespace Hyper.Api.FakeResponses
{
    public static class LogFakeResponse
    {
        public static LogResponse GetFake_Event()
        {
            return new LogResponse
            {              
                Id = Guid.Parse("2f0bd0cd-6b95-4759-afbc-d25c570d823c"),
                LogLevel = LogLevel.Info,
                Message = Event.ImportCurrencies.ToString(),
                CreationTime = DateTime.Parse("2018-09-14T14:05")
            };
        }
        public static LogResponse GetFake_Error()
        {
            return new LogResponse
            {
                Id = Guid.Parse("2f0bd0cd-6b95-4759-afbc-d25c570d824d"),
                LogLevel = LogLevel.Error,
                Message = "Object reference not set to an instance of an object",
                CreationTime = DateTime.Parse("2018-09-14T14:05")
            };
        }
        public static IEnumerable<LogResponse> GetFake_List()
        {
            return new List<LogResponse>
            {
                GetFake_Event(),
                GetFake_Error()
            };
        }
    }
}
