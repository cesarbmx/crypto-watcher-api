using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Users.Requests;
using CryptoWatcher.Application.Users.Responses;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Persistence.Repositories;
using CryptoWatcher.Shared.Exceptions;
using CryptoWatcher.Shared.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Users.Handlers
{
    public class AddUserHandler : IRequestHandler<AddUserRequest, UserResponse>
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IRepository<User> _userRepository;
        private readonly ILogger<AddUserRequest> _logger;
        private readonly IMapper _mapper;

        public AddUserHandler(
            MainDbContext mainDbContext,
            IRepository<User> userRepository,
            ILogger<AddUserRequest> logger,
            IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<UserResponse> Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            // Get user
            var user = await _userRepository.GetSingle(request.UserId);

            // Check if user exists
            if (user != null) throw new ConflictException(UserMessage.UserExists);

            // Add user
            user = new User(request.UserId);
            _userRepository.Add(user);

            // Save
            await _mainDbContext.SaveChangesAsync(cancellationToken);

            // Log into Splunk
            _logger.LogSplunkInformation(request);

            // Response
            var response = _mapper.Map<UserResponse>(user);

            // Return
            return response;
        }
    }
}
