using System;
using CryptoWatcher.Application.Responses;
using MediatR;

namespace CryptoWatcher.Application.Requests
{
    public class GetNotificationRequest : IRequest<NotificationResponse>
    {
        public Guid NotificationId { get; set; }
    }
}
