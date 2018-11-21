using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Repositories
{
    public interface ILogRepository : IRepository<Log>
    {
        Task<List<Log>> GetFromDate(DateTime dateTime);
    }
}