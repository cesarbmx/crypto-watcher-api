using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CesarBmx.CryptoWatcher.Application.ConflictReasons;
using CesarBmx.Shared.Application.Exceptions;
using CesarBmx.Shared.Common.Extensions;
using CesarBmx.Shared.Logging.Extensions;
using CesarBmx.CryptoWatcher.Application.Requests;
using CesarBmx.CryptoWatcher.Application.Messages;
using CesarBmx.CryptoWatcher.Persistence.Contexts;
using CesarBmx.Shared.Application.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CesarBmx.CryptoWatcher.Application.Services
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

        public async Task<List<Responses.User>> GetUsers()
        {
            // Get all users
            var users = await _mainDbContext.Users.ToListAsync();

            // Response
            var response = _mapper.Map<List<Responses.User>>(users);

            // Return
            return response;
        }
        public async Task<Responses.User> GetUser(string userId)
        {
            // Get user
            var user = await _mainDbContext.Users.FindAsync(userId);

            // Throw NotFound if it does not exist
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Response
            var response = _mapper.Map<Responses.User>(user);

            // Return
            return response;
        }
        public async Task<Responses.User> AddUser(AddUser request)
        {
            // Get user
            var user = await _mainDbContext.Users.FindAsync(request.UserId);

            // Check if it exists
            if (user != null) throw new ConflictException(new Conflict<AddUserConflictReason>(AddUserConflictReason.USER_ALREADY_EXISTS, UserMessage.UserAlreadyExists));

            // Time
            var now = DateTime.UtcNow.StripSeconds();

            // Create
            user = new Domain.Models.User(request.UserId, request.PhoneNumber, now);

            // Add user
            _mainDbContext.Users.Add(user);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Log into Splunk
            _logger.LogSplunkInformation(request);

            // Response
            var response = _mapper.Map<Responses.User>(user);

            // Return
            return response;
        }
    }
}
