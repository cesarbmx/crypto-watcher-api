using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Application.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Exceptions;
using CryptoWatcher.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Services
{
    public class UserService
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;

        public UserService(
            MainDbContext mainDbContext,
            ILogger<UserService> logger,
            IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<UserResponse>> GetAllUsers()
        {
            // Get all users
            var users = await _mainDbContext.Users.ToListAsync();

            // Response
            var response = _mapper.Map<List<UserResponse>>(users);

            // Return
            return response;
        }
        public async Task<UserResponse> GetUser(string userId)
        {
            // Get user
            var user = await _mainDbContext.Users.FindAsync(userId);

            // Throw NotFoundException if it does not exist
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Response
            var response = _mapper.Map<UserResponse>(user);

            // Return
            return response;
        }
        public async Task<UserResponse> AddUser(AddUserRequest request)
        {
            // Get user
            var user = await _mainDbContext.Users.FindAsync(request.UserId);

            // Check if it exists
            if (user != null) throw new ConflictException(UserMessage.UserAlreadyExists);

            // Create
            user = new User(request.UserId, DateTime.Now);

            // Add user
            _mainDbContext.Users.Add(user);

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
