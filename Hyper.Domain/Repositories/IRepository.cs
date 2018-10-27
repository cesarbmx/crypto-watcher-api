using System.Collections.Generic;
using System.Threading.Tasks;
using Hyper.Domain.Models;

namespace Hyper.Domain.Repositories
{
    public interface IRepository<T> where T:IEntity
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        void Add(T t);
    }
}