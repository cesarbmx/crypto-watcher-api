using System;
using System.Collections.Generic;
using CryptoWatcher.Api.Responses;

namespace CryptoWatcher.Api.FakeResponses
{
    public static class LogFakeResponse
    {
        public static LogResponse GetFake_Log()
        {
            return new LogResponse
            {              
                LogId = "2779cf8051-381f-4834-93dc-ece6345dde33",
                Resource = "Cache",
                Action = "Add",
                Json = "{}",
                CreationTime = DateTime.Parse("2018-09-14T14:05")
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
