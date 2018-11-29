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
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetUsers()
        {
            // Get users
            return await _userRepository.GetAll();
        }
        public async Task<User> GetUser(string id)
        {
            // Get user
            var user = await _userRepository.GetById(id);

            // Throw NotFound exception if it does not exist
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Return
            return user;
        }
        public async Task<User> AddUser(string id)
        {
            // Add user
            var user = new User(id);
            _userRepository.Add(user);

            // Return
            return await Task.FromResult(user);
        }
    }
}
