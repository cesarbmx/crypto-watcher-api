using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CesarBmx.Shared.Application.Exceptions;
using CesarBmx.Shared.Logging.Extensions;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Application.Messages;
using CryptoWatcher.Domain.Models;
using CesarBmx.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Services
{
    public class UserService
    {
        private readonly DbContext _dbContext;
        private readonly IRepository<User> _userRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;

        public UserService(
            DbContext dbContext,
            IRepository<User> userRepository,
            ILogger<UserService> logger,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<UserResponse>> GetAllUsers()
        {
            // Get all users
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

            // Throw NotFound if it does not exist
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

            // Check if it exists
            if (user != null) throw new ConflictException(UserMessage.UserAlreadyExists);

            // Time
            var time = DateTime.Now;

            // Create
            user = new User(request.UserId, time);

            // Add user
            _userRepository.Add(user);

            // Save
            await _dbContext.SaveChangesAsync();

            // Log into Splunk
            _logger.LogSplunkInformation(request);

            // Response
            var response = _mapper.Map<UserResponse>(user);

            // Return
            return response;
        }
    }
}
