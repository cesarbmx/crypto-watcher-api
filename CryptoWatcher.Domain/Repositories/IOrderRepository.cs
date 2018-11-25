using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetByOrderId(string orderId);
        Task<List<Order>> GetByUserId(string userId);
    }
}