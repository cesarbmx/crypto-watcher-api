using System;
using System.Collections.Generic;
using CryptoWatcher.Application.Responses;

namespace CryptoWatcher.Application.FakeResponses
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
                CreatedBy = "system",
                CreationTime = DateTime.Parse("2018-09-14T14:05"),
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
