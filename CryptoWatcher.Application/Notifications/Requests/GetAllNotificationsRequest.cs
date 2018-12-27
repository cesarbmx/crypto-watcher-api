using System.Collections.Generic;
using CryptoWatcher.Application.Notifications.Responses;
using MediatR;

namespace CryptoWatcher.Application.Notifications.Requests
{
    public class GetAllNotificationsRequest : IRequest<List<NotificationResponse>>
    {
        public string UserId { get; set; }
    }
}
