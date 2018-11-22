using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUserId(string userId);
    }
}