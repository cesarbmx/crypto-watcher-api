using AutoMapper;
using CesarBmx.CryptoWatcher.Domain.Models;

namespace CesarBmx.CryptoWatcher.Application.Automapper
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<Order, Responses.Order>();
        }
    }
}
