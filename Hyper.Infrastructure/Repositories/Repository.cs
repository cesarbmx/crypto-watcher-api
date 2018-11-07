using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Hyper.Domain.Models;
using Hyper.Persistence.Contexts;
using Hyper.Domain.Repositories;
using System.Collections.Generic;

namespace Hyper.Persistence.Repositories
{
    public class Repository<TEntity>: IRepository<TEntity> where TEntity : Entity
    {
        private readonly MainDbContext _mainDbContext;

        public Repository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _mainDbContext.Set<TEntity>().ToListAsync();
        }
        public async Task<TEntity> GetByKey(string id)
        {
            return await _mainDbContext.Set<TEntity>().FindAsync(id);
        }
        public void Add(TEntity entity)
        {
            _mainDbContext.Set<TEntity>().Add(entity);
        }
        public void Remove(TEntity entity)
        {
            _mainDbContext.Set<TEntity>().Remove(entity);
        }
    }
}
