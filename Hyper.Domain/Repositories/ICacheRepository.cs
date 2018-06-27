using System.Threading.Tasks;
using Hyper.Domain.Models;

namespace Hyper.Domain.Repositories
{
    public interface ICacheRepository
    {
        Task<Cache> GetByKey(string key);
        void Add(Cache cache);
    }
}