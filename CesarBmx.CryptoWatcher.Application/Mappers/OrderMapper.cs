using AutoMapper;
using CesarBmx.CryptoWatcher.Domain.Models;

namespace CesarBmx.CryptoWatcher.Application.Mappers
{
    public class OrderMapper : Profile
    {
        public OrderMapper()
        {
            CreateMap<Order, Responses.Order>();
        }
    }
}
