using System.Collections.Generic;
using CryptoWatcher.Api.Responses;
using MediatR;

namespace CryptoWatcher.Api.Requests
{
    public class GetNotificationsRequest : IRequest<List<NotificationResponse>>
    {
        
    }
}
