using System;
using System.Collections.Generic;
using CesarBmx.Shared.Common.Extensions;
using CryptoWatcher.Application.Responses;

namespace CryptoWatcher.Application.FakeResponses
{
    public static class FakeNotification
    {
        public static Notification GetFake_master()
        {
            return new Notification
            {
                NotificationId = Guid.NewGuid(),
                UserId = "master",
                Message = "Test message",
                PhoneNumber = "+34666555555",
                Time = DateTime.UtcNow.StripSeconds(),
                SentTime = null
            };
        }
        public static Notification GetFake_cesarbmx()
        {
            return new Notification
            {
                NotificationId = Guid.NewGuid(),
                UserId = "cesarbmx",
                Message = "Test message",
                PhoneNumber = "+34666666666",
                Time = DateTime.UtcNow.StripSeconds(),
                SentTime = null
            };
        }
        public static List<Notification> GetFake_List()
        {
            return new List<Notification>
            {
                GetFake_master(),
                GetFake_cesarbmx()
            };
        }
    }
}
