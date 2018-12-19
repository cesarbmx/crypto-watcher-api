using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CryptoWatcher.Persistence.Contexts;
using System.Collections.Generic;
using System.Linq;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Persistence.Repositories
{
    public class LineRepository : Repository<Line>
    {
        private readonly DbSet<Line> _dbSet;

        public LineRepository(MainDbContext mainDbContext)
            :base(mainDbContext)
        {
            _dbSet = mainDbContext.Set<Line>();
        }

        public async Task<List<Line>> GetCurrentLines()
        {
            var maxDate = _dbSet.OrderByDescending(t => t.CreationTime).First().CreationTime;
            return await _dbSet.Where(x => x.CreationTime == maxDate).ToListAsync();
        }
    }
}
