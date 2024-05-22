using System;
using System.Collections.Generic;
using CesarBmx.CryptoWatcher.Application.Responses;
using CesarBmx.Shared.Common.Extensions;

namespace CesarBmx.CryptoWatcher.Application.FakeResponses
{
    public static class FakeUserLog
    {

        public static UserLogResponse GetFake_User1()
        {
            return new UserLogResponse
            {
                LogId = Guid.NewGuid(),
                UserId = "cesarbmx",
                Action = "AddWatcher",
                Description = "Watcher added (BTC, PRICE)",
                CreatedAt = DateTime.UtcNow.StripSeconds()
            };
        }
        public static List<UserLogResponse> GetFake_List()
        {
            return new List<UserLogResponse>
            {
                GetFake_User1()
            };
        }
    }
}
