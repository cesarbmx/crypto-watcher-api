using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Api.Requests;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Repositories;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Exceptions;
using CryptoWatcher.Shared.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Api.Handlers
{
    public class AddUserHandler : IRequestHandler<AddUserRequest, UserResponse>
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IRepository<User> _userRepository;
        private readonly ILogger<AddUserHandler> _logger;
        private readonly IMapper _mapper;

        public AddUserHandler(
            MainDbContext mainDbContext,
            IRepository<User> userRepository,
            ILogger<AddUserHandler> logger,
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
            var user = await _userRepository.GetById(request.Id);

            // Check if user exists
            if (user != null) throw new ConflictException(UserMessage.UserExists);
           
            // Add user
             user = new User(request.Id);
            _userRepository.Add(user);

            // Save
            await _mainDbContext.SaveChangesAsync(cancellationToken);

            // Log into Splunk
            _logger.LogSplunkInformation(nameof(AddUserRequest), request);

            // Response
            var response = _mapper.Map<UserResponse>(user);
           
            // Return
            return response;
        }
    }
}
