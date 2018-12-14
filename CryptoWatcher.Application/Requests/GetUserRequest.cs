using CryptoWatcher.Application.Responses;
using MediatR;

namespace CryptoWatcher.Application.Requests
{
    public class GetUserRequest : IRequest<UserResponse>
    {
        public string UserId { get; set; }
    }
}
