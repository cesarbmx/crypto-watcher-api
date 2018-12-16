using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CryptoWatcher.Shared.Domain
{
    public interface IRepository<TEntity> where TEntity: IEntity
    {
        Task<List<TEntity>> GetAll();
        Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetSingle(string id);
        Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> expression);
        void Add(TEntity entity);
        void AddRange(List<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(List<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(List<TEntity> entities);
    }
}