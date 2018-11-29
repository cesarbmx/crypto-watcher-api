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
        void Add(TEntity entity);
        void Remove(TEntity entity);
    }
}