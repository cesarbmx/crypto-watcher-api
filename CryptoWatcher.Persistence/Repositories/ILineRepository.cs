using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Persistence.Repositories
{
    public interface ILineRepository: IRepository<DataPoint>
    {
        Task<List<DataPoint>> GetCurrentLines();
    }
}