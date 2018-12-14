using System.Collections.Generic;
using CryptoWatcher.Application.Responses;
using MediatR;

namespace CryptoWatcher.Application.Requests
{
    public class GetAllNotificationsRequest : IRequest<List<NotificationResponse>>
    {
        public string UserId { get; set; }
    }
}
