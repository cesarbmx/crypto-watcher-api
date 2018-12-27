using System.ComponentModel.DataAnnotations;
using CryptoWatcher.Application.Users.Responses;
using MediatR;

namespace CryptoWatcher.Application.Users.Requests
{
    public class AddUserRequest : IRequest<UserResponse>
    {
        [Required] public string UserId { get; set; }
    }
}
