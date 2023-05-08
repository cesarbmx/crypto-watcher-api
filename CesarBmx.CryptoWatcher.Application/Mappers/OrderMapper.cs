using AutoMapper;
using CesarBmx.CryptoWatcher.Domain.Models;
using CesarBmx.Shared.Messaging.Ordering.Events;

namespace CesarBmx.CryptoWatcher.Application.Mappers
{
    public class OrderMapper : Profile
    {
        public OrderMapper()
        {
            // Model to Response
            CreateMap<Order, Responses.Order>();

            // Model to Event
            CreateMap<Order, OrderPlaced>();

            CreateMap<OrderSubmitted, Order>();
        }
    }
}
