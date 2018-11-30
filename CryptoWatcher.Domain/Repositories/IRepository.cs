using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity: Entity
    {
        Task<List<TEntity>> GetAll();
        Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetById(string id);
        Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> expression);
        void Add(TEntity entity);
        void AddRange(List<TEntity> entities);
        void Remove(TEntity entity);
    }
}