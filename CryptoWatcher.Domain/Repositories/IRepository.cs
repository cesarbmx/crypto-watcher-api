using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity: Entity
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(string id);
        void Add(TEntity entity);
        void Remove(TEntity entity);
    }
}