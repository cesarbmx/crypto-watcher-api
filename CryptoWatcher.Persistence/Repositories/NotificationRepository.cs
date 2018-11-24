using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        private readonly MainDbContext _mainDbContext;

        public NotificationRepository(MainDbContext mainDbContext)
            : base(mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task<Notification> GetByNotificationId(string notificationId)
        {
            return await _mainDbContext.Notifications.FirstOrDefaultAsync(x => x.NotificationId == notificationId);
        }

        public async Task<List<Notification>> GetPendingNotifications()
        {
            return await _mainDbContext.Notifications.Where(x => x.WhatsappSent == false).ToListAsync();
        }
    }
}
