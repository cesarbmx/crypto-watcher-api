using System;
using CryptoWatcher.Application.Notifications.Responses;
using MediatR;

namespace CryptoWatcher.Application.Notifications.Requests
{
    public class GetNotificationRequest : IRequest<NotificationResponse>
    {
        public Guid NotificationId { get; set; }
    }
}
