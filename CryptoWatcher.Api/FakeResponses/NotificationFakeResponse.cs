using System.Collections.Generic;
using CryptoWatcher.Api.Responses;

namespace CryptoWatcher.Api.FakeResponses
{
    public static class NotificationFakeResponse
    {
        public static NotificationResponse GetFake_Cesarbmx()
        {
            return new NotificationResponse
            {
                NotificationId = "2779cf8051-381f-4834-93dc-ece6345dde33",
                UserId = "cesarbmx",
                Message = "Test",
                PhoneNumber = "+34636860935",
                WhatsappSent = false,
                WhatsappSentTime = null
            };
        }
        public static NotificationResponse GetFake_Johny()
        {
            return new NotificationResponse
            {
                NotificationId = "2779cf8051-381f-4834-93dc-ece6345dde33",
                UserId = "johny.melavo",
                Message = "Test",
                PhoneNumber = "+34666666666",
                WhatsappSent = false,
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
