using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CryptoWatcher.Shared.Contexts;
using CryptoWatcher.Shared.Models;

namespace CryptoWatcher.Shared.Repositories
{
    public class Repository<TEntity>: IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbSet<TEntity> _dbSet;

        public Repository(IContext dbContext)
        {
            _dbSet = dbContext.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAll()
        {
            // Get all
            return await _dbSet.ToListAsync();
        }
        public async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression)
        {
            // Get all by expression
            return await _dbSet.Where(expression).ToListAsync();
        }
        public async Task<TEntity> GetSingle(object id)
        {
            // Get by id
            return await _dbSet.FindAsync(id);
        }
        public async Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> expression)
        {
            // Get single by expression
            return await _dbSet.FirstOrDefaultAsync(expression);
        }
        public void Add(TEntity entity)
        {
            // Add
            _dbSet.Add(entity);
        }
        public void AddRange(List<TEntity> entities)
        {
            // Add
            foreach (var entity in entities)
            {
                Add(entity);
            }
        }
        public void Update(TEntity entity)
        {
            // Update
            _dbSet.Update(entity);
        }
        public void UpdateRange(List<TEntity> entities)
        {
            // Update
            foreach (var entity in entities)
            {
                Update(entity);
            }
        }
        public void Remove(TEntity entity)
        {
            // Remove
            _dbSet.Remove(entity);
        }
        public void RemoveRange(List<TEntity> entities)
        {
            // Remove
            foreach (var entity in entities)
            {
                Remove(entity);
            }
        }
    }
}
