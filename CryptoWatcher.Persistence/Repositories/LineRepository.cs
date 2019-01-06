using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CryptoWatcher.Persistence.Contexts;
using System.Collections.Generic;
using System.Linq;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Persistence.Repositories
{
    public class LineRepository : Repository<DataPoint>
    {
        private readonly DbSet<DataPoint> _dbSet;

        public LineRepository(MainDbContext mainDbContext)
            :base(mainDbContext)
        {
            _dbSet = mainDbContext.Set<DataPoint>();
        }

        public async Task<List<DataPoint>> GetCurrentLines()
        {
            var row = await _dbSet.OrderByDescending(t => t.Time).FirstOrDefaultAsync();
            if(row == null) return new List<DataPoint>();
            var maxDate = row.Time;
            return await _dbSet.Where(x => x.Time == maxDate).ToListAsync();
        }
    }
}
