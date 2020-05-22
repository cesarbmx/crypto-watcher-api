using AutoMapper;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Application.Automapper
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<Order, Responses.Order>();
        }
    }
}
