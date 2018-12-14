using CryptoWatcher.Application.Responses;
using MediatR;

namespace CryptoWatcher.Application.Requests
{
    public class GetNotificationRequest : IRequest<NotificationResponse>
    {
        public string NotificationId { get; set; }
    }
}
