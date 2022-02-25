using AutoMapper;
using CesarBmx.CryptoWatcher.Domain.Models;

namespace CesarBmx.CryptoWatcher.Application.Mappers
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, Responses.User>();
        }
    }
}
