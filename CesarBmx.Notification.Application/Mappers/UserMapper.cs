using AutoMapper;
using CesarBmx.Notification.Domain.Models;

namespace CesarBmx.Notification.Application.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, Responses.User>();
        }
    }
}
