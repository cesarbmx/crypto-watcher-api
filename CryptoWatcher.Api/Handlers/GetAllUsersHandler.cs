using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Api.Requests;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Domain;
using MediatR;

namespace CryptoWatcher.Api.Handlers
{
    public class GetAllUsersHandler : IRequestHandler<GetUsersRequest, List<UserResponse>>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsersHandler(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserResponse>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            // Get users
            var users = await _userRepository.GetAll();

            // Response
            var response = _mapper.Map<List<UserResponse>>(users);

            // Return
            return response;
        }
    }
}
