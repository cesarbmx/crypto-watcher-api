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
        private readonly DbSet<TEntity> _dbSet;
        private readonly DbSet<Log> _dbLogSet;

        public Repository(MainDbContext mainDbContext)
        {
            _dbSet = mainDbContext.Set<TEntity>();
            _dbLogSet = mainDbContext.Log;
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<TEntity> GetByKey(string id)
        {
            return await _dbSet.FindAsync(id);
        }
        public void Add(TEntity entity)
        {
            // Add
            _dbSet.Add(entity);

            // Log
            _dbLogSet.Add(new Log(entity, "Add"));
        }
        public void Update(TEntity entity)
        {
            // Log
            _dbLogSet.Add(new Log(entity, "Update"));
        }
        public void Remove(TEntity entity)
        {
            // Remove
            _dbSet.Remove(entity);

            // Log
            _dbLogSet.Add(new Log(entity, "Remove"));
        }
    }
}
