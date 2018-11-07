using System.Collections.Generic;
using System.Threading.Tasks;
using Hyper.Domain.Models;

namespace Hyper.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity: Entity
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetByKey(string id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}