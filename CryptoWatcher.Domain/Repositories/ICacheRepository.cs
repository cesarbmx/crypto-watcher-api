using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Repositories
{
    public interface ICacheRepository : IRepository<Cache>
    {
        Task<Cache> GetByKey(string key);
    }
}