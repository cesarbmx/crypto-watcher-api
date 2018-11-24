using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Repositories
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<Notification> GetByNotificationId(string notificationId);
        Task<List<Notification>> GetPendingNotifications();
    }
}