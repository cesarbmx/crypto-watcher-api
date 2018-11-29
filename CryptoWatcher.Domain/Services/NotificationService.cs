using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Repositories;
using CryptoWatcher.Shared.Exceptions;

namespace CryptoWatcher.Domain.Services
{
    public class NotificationService
    {
        private readonly IRepository<Notification> _notificationRepository;
        private readonly UserService _userService;

        public NotificationService(IRepository<Notification> notificationRepository, UserService userService)
        {
            _notificationRepository = notificationRepository;
            _userService = userService;
        }

        public async Task<List<Notification>> GetNotifications()
        {
            // Get notification
            return await _notificationRepository.GetAll();
        }
        public async Task<Notification> GetNotification(string notificationId)
        {
            // Get notification
            var notification = await _notificationRepository.GetById(notificationId);

            // Throw NotFound exception if it does not exist
            if (notification == null) throw new NotFoundException(NotificationMessage.NotificationNotFound);

            // Return
            return notification;
        }
        public async Task<List<Notification>> GetPendingNotifications()
        {
            // Get pending notifications
            var notifications = await _notificationRepository.Get(NotificationExpression.PendingNotification());

            // Return
            return notifications;
        }
        public async Task<Notification> AddNotification(string userId, string message)
        {
            // Get user
            var user = await _userService.GetUser(userId);

            // Add notification
            var notification = new Notification(user.Id, message);
            _notificationRepository.Add(notification);

            // Return
            return await Task.FromResult(notification);
        }
    }
}
