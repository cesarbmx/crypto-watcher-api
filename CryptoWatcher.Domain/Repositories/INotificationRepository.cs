using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Repositories
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<Notification> GetByNotificationId(string notificationId);
    }
}