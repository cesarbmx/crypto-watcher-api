using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Contexts;
using CryptoWatcher.Shared.Repositories;

namespace CryptoWatcher.Persistence.Repositories
{
    public class LineRepository : Repository<Line>
    {
        private readonly DbSet<Line> _dbSet;

        public LineRepository(IContext mainDbContext)
            :base(mainDbContext)
        {
            _dbSet = mainDbContext.Set<Line>();
        }

        public async Task<List<Line>> GetCurrentLines()
        {
            var maxDate = _dbSet.OrderByDescending(t => t.Time).First().Time;
            return await _dbSet.Where(x => x.Time == maxDate).ToListAsync();
        }
    }
}
