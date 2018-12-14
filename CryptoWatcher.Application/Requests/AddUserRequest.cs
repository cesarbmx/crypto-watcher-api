
using CryptoWatcher.Application.Responses;
using MediatR;

namespace CryptoWatcher.Application.Requests
{
    public class AddUserRequest: IRequest<UserResponse>
    {
    public string UserId { get; set; }
    }
}
