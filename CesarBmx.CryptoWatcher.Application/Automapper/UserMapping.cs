using AutoMapper;
using CesarBmx.CryptoWatcher.Domain.Models;

namespace CesarBmx.CryptoWatcher.Application.Automapper
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, Responses.User>();
        }
    }
}
