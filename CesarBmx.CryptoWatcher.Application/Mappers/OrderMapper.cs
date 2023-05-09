using AutoMapper;
using CesarBmx.CryptoWatcher.Domain.Models;
using CesarBmx.Shared.Messaging.Ordering.Commands;
using CesarBmx.Shared.Messaging.Ordering.Events;

namespace CesarBmx.CryptoWatcher.Application.Mappers
{
    public class OrderMapper : Profile
    {
        public OrderMapper()
        {
            // Model to Response
            CreateMap<Order, Responses.Order>();

            // Model to Command
            CreateMap<Order, SubmitOrder>();
        }
    }
}
