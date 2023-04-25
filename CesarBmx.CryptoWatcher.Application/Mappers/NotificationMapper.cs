using AutoMapper;
using CesarBmx.CryptoWatcher.Domain.Models;
using CesarBmx.Shared.Messaging.Notification.Events;

namespace CesarBmx.CryptoWatcher.Application.Mappers
{
    public class NotificationMapper : Profile
    {
        public NotificationMapper()
        {
            // Model to Response
            CreateMap<Notification, Responses.Notification>();

            // Model to Event
            CreateMap<Notification, MessageSent>();
        }
    }
}
