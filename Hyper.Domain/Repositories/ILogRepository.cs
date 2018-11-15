using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hyper.Domain.Models;

namespace Hyper.Domain.Repositories
{
    public interface ILogRepository : IRepository<Log>
    {
        Task<List<Log>> GetFromDate(DateTime dateTime);
    }
}