using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Repositories;
using CryptoWatcher.Shared.Exceptions;

namespace CryptoWatcher.Domain.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetUsers(string userId)
        {
            // Get user
            return await _userRepository.Get();
        }
        public async Task<User> GetUser(string userId)
        {
            // Get user by id
            var user = await _userRepository.GetByUserId(userId);

            // Throw NotFound exception if it does not exist
            if (user == null) throw new NotFoundException(UserMessages.NotFound);

            // Return
            return user;
        }
        public async Task<User> AddUser(string userId)
        {
            // Add user
            var user = new User(userId);
            _userRepository.Add(user);

            // Return
            return await Task.FromResult(user);
        }
    }
}
