using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly MainDbContext _mainDbContext;

        public UserRepository(MainDbContext mainDbContext)
            : base(mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task<List<User>> Get(string userId)
        {
            return await _mainDbContext.Users.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
