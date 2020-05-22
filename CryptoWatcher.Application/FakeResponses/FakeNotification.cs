using System;
using System.Collections.Generic;
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
                WhatsappSentTime = null
            };
        }
        public static Notification GetFake_cesar12()
        {
            return new Notification
            {
                NotificationId = Guid.NewGuid(),
                UserId = "cesar12",
                Message = "Test message",
                PhoneNumber = "+34666666666",
                WhatsappSentTime = null
            };
        }
        public static List<Notification> GetFake_List()
        {
            return new List<Notification>
            {
                GetFake_master(),
                GetFake_cesar12()
            };
        }
    }
}
