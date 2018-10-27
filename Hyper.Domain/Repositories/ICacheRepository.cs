using System.Threading.Tasks;
using Hyper.Domain.Models;

namespace Hyper.Domain.Repositories
{
    public interface ICacheRepository : IRepository<Cache>
    {
        Task<Cache> GetByKey(string key);
    }
}