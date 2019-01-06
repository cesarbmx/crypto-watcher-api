using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Persistence.Repositories
{
    public interface IRepository<TEntity> where TEntity: IEntity
    {
        Task<List<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetSingle(object id, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includeProperties);
        void Add(TEntity entity);
        void AddRange(List<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(List<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(List<TEntity> entities);
    }
}