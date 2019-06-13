using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Persistence.Repositories
{
    public interface IRepository<TEntity> where TEntity: IEntity
    {
        Task<List<TEntity>> GetAll();
        Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetSingle(object id);
        Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> expression);
        Task<DateTime> GetNewestTime();
        void Add(TEntity entity, DateTime time);
        void AddRange(List<TEntity> entities, DateTime time);
        void Update(TEntity entity, DateTime time);
        void UpdateRange(List<TEntity> entities, DateTime time);
        void Remove(TEntity entity, DateTime time);
        void RemoveRange(List<TEntity> entities, DateTime time);
        void UpdateCollection(List<TEntity> currentEntities, List<TEntity> newEntities, DateTime time);
    }
}