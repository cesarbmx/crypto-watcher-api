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
                Id = 1,
                ModelName = "Cache",
                ActionName = "Add",
                ModelJson = "{}",
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
