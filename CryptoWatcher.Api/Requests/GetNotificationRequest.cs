using CryptoWatcher.Api.Responses;
using MediatR;

namespace CryptoWatcher.Api.Requests
{
    public class GetNotificationRequest : IRequest<NotificationResponse>
    {
        public string NotificationId { get; set; }
    }
}
