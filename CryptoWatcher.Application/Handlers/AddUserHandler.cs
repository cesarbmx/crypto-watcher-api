using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Contexts;
using CryptoWatcher.Shared.Exceptions;
using CryptoWatcher.Shared.Extensions;
using CryptoWatcher.Shared.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Handlers
{
    public class AddUserHandler : IRequestHandler<AddUserRequest, UserResponse>
    {
        private readonly IContext _context;
        private readonly IRepository<User> _userRepository;
        private readonly ILogger<AddUserRequest> _logger;
        private readonly IMapper _mapper;

        public AddUserHandler(
            IContext context,
            IRepository<User> userRepository,
            ILogger<AddUserRequest> logger,
            IMapper mapper)
        {
            _context = context;
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
            await _context.SaveChangesAsync(cancellationToken);

            // Log into Splunk
            _logger.LogSplunkInformation(request);

            // Response
            var response = _mapper.Map<UserResponse>(user);
           
            // Return
            return response;
        }
    }
}
