using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Persistence.Repositories;
using CryptoWatcher.Shared.Exceptions;
using CryptoWatcher.Shared.Extensions;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Services
{
    public class UserService
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IRepository<User> _userRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;

        public UserService(
            MainDbContext mainDbContext,
            IRepository<User> userRepository,
            ILogger<UserService> logger,
            IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<UserResponse>> GetAllUsers()
        {
            // Get users
            var users = await _userRepository.GetAll();

            // Response
            var response = _mapper.Map<List<UserResponse>>(users);

            // Return
            return response;
        }
        public async Task<UserResponse> GetUser(string userId)
        {
            // Get user
            var user = await _userRepository.GetSingle(userId);

            // Throw NotFound exception if the currency does not exist
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Response
            var response = _mapper.Map<UserResponse>(user);

            // Return
            return response;
        }
        public async Task<UserResponse> AddUser(AddUserRequest request)
        {
            // Get user
            var user = await _userRepository.GetSingle(request.UserId);

            // Check if user exists
            if (user != null) throw new ConflictException(UserMessage.UserAlreadyExists);

            // Add user
            user = new User(request.UserId);
            _userRepository.Add(user);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Log into Splunk
            _logger.LogSplunkRequest(request);

            // Response
            var response = _mapper.Map<UserResponse>(user);

            // Return
            return response;
        }
    }
}
