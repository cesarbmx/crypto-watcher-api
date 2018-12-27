using System.ComponentModel.DataAnnotations;
using CryptoWatcher.Application.Notifications.Responses;
using MediatR;

namespace CryptoWatcher.Application.Notifications.Requests
{
    public class AddNotificationRequest : IRequest<NotificationResponse>
    {
        [Required] public string Id { get; set; }
        [Required] public string Message { get; set; }
    }
}
