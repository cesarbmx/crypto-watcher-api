using System;
using System.Collections.Generic;
using CryptoWatcher.Application.Responses;

namespace CryptoWatcher.Application.FakeResponses
{
    public static class NotificationFakeResponse
    {
        public static NotificationResponse GetFake_master()
        {
            return new NotificationResponse
            {
                NotificationId = Guid.NewGuid(),
                UserId = "master",
                Message = "Test message",
                PhoneNumber = "+34666555555",
                WhatsappSentTime = null
            };
        }
        public static NotificationResponse GetFake_cesar12()
        {
            return new NotificationResponse
            {
                NotificationId = Guid.NewGuid(),
                UserId = "cesar12",
                Message = "Test message",
                PhoneNumber = "+34666666666",
                WhatsappSentTime = null
            };
        }
        public static List<NotificationResponse> GetFake_List()
        {
            return new List<NotificationResponse>
            {
                GetFake_master(),
                GetFake_cesar12()
            };
        }
    }
}
