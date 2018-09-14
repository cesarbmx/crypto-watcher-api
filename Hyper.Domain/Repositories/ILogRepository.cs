using System.Collections.Generic;
using System.Threading.Tasks;
using Hyper.Domain.Models;

namespace Hyper.Domain.Repositories
{
    public interface ILogRepository
    {
        Task<List<Log>> GetLog();
        void Add(Log log);
    }
}