using AutoMapper;
using CesarBmx.CryptoWatcher.Domain.Models;

namespace CesarBmx.CryptoWatcher.Application.Mappers
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<Order, Responses.Order>();
        }
    }
}
