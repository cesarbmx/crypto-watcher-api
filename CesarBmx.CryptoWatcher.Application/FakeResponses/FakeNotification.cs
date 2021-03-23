using System;
using System.Collections.Generic;
using CesarBmx.Shared.Common.Extensions;
using CesarBmx.CryptoWatcher.Application.Resources;

namespace CesarBmx.CryptoWatcher.Application.FakeResponses
{
    public static class FakeNotification
    {
        public static Notification GetFake_Master()
        {
            return new Notification
            {
                NotificationId = 1,
                UserId = "Master",
                Message = "Test message",
                PhoneNumber = "+34666555555",
                Time = DateTime.UtcNow.StripSeconds(),
                SentTime = null
            };
        }
        public static Notification GetFake_CesarBmx()
        {
            return new Notification
            {
                NotificationId = 2,
                UserId = "CesarBmx",
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
                GetFake_Master(),
                GetFake_CesarBmx()
            };
        }
    }
}
