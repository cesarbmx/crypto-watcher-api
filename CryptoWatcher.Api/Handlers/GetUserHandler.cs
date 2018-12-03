using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Api.Requests;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Repositories;
using CryptoWatcher.Shared.Exceptions;
using MediatR;

namespace CryptoWatcher.Api.Handlers
{
    public class GetUserHandler : IRequestHandler<GetUserRequest, UserResponse>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public GetUserHandler(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            // Get user
            var user = await _userRepository.GetSingle(request.UserId);

            // Throw NotFound exception if the currency does not exist
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Response
            var response = _mapper.Map<UserResponse>(user);

            // Return
            return response;
        }
    }
}
