using System.Collections.Generic;
using CryptoWatcher.Application.Users.Responses;
using MediatR;

namespace CryptoWatcher.Application.Users.Requests
{
    public class GetAllUsersRequest : IRequest<List<UserResponse>>
    {
        
    }
}
