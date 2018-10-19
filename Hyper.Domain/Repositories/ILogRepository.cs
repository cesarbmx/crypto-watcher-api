using System.Collections.Generic;
using System.Threading.Tasks;
using Hyper.Domain.Models;

namespace Hyper.Domain.Repositories
{
    public interface ILogRepository
    {
        Task<IEnumerable<Log>> GetLog();
        void Add(Log log);
    }
}