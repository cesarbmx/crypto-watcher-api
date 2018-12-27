using System;
using System.Collections.Generic;
using CryptoWatcher.Application.Notifications.Responses;

namespace CryptoWatcher.Application.Notifications.FakeResponses
{
    public static class NotificationFakeResponse
    {
        public static NotificationResponse GetFake_Cesarbmx()
        {
            return new NotificationResponse
            {
                NotificationId = Guid.NewGuid(),
                UserId = "johny.melavo",
                Message = "Test",
                PhoneNumber = "+34666666666",
                WhatsappSentTime = null
            };
        }
        public static NotificationResponse GetFake_Johny()
        {
            return new NotificationResponse
            {
                NotificationId = Guid.NewGuid(),
                UserId = "chin.champu",
                Message = "Test",
                PhoneNumber = "+34666666666",
                WhatsappSentTime = null
            };
        }
        public static List<NotificationResponse> GetFake_List()
        {
            return new List<NotificationResponse>
            {
                GetFake_Cesarbmx(),
                GetFake_Johny()
            };
        }
    }
}
