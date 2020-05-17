using AutoMapper;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Application.Automapper
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserResponse>();
        }
    }
}
