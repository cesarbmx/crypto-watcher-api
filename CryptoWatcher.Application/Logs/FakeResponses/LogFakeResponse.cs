using System;
using System.Collections.Generic;
using CryptoWatcher.Application.Logs.Responses;

namespace CryptoWatcher.Application.Logs.FakeResponses
{
    public static class LogFakeResponse
    {
        public static LogResponse GetFake_Log()
        {
            return new LogResponse
            {              
                LogId = Guid.NewGuid(),
                Action = "Add",
                Entity = "Cache",
                EntityId = "Currencies",
                Time = DateTime.Now,
                Json = "{}"
            };
        }
        public static List<LogResponse> GetFake_List()
        {
            return new List<LogResponse>
            {
                GetFake_Log()
            };
        }
    }
}
