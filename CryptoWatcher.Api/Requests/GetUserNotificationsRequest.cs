using System.Collections.Generic;
using CryptoWatcher.Api.Responses;
using MediatR;

namespace CryptoWatcher.Api.Requests
{
    public class GetUserNotificationsRequest : IRequest<List<NotificationResponse>>
    {
        public string Id { get; set; }
    }
}
