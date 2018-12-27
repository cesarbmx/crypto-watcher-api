using CryptoWatcher.Application.Users.Responses;
using MediatR;

namespace CryptoWatcher.Application.Users.Requests
{
    public class GetUserRequest : IRequest<UserResponse>
    {
        public string UserId { get; set; }
    }
}
