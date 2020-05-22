using AutoMapper;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Application.Automapper
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, Responses.User>();
        }
    }
}
