using AutoMapper;
using CesarBmx.Notification.Domain.Models;

namespace CesarBmx.Notification.Application.Mappers
{
    public class OrderMapper : Profile
    {
        public OrderMapper()
        {
            CreateMap<Order, Responses.Order>();
        }
    }
}
