using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> Get(string userId);
    }
}