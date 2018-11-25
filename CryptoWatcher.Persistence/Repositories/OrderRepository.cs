using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly MainDbContext _mainDbContext;

        public OrderRepository(MainDbContext mainDbContext)
            : base(mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task<Order> GetByOrderId(string orderId)
        {
            return await _mainDbContext.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId);
        }
        public async Task<List<Order>> GetByUserId(string userId)
        {
            return await _mainDbContext.Orders.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
