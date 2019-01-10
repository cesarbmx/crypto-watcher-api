using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Application.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Repositories;
using CryptoWatcher.Shared.Exceptions;

namespace CryptoWatcher.Application.Services
{
    public class NotificationService
    {
        private readonly IRepository<Notification> _notificationRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public NotificationService(
            IRepository<Notification> notificationRepository,
            IRepository<User> userRepository,
            IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<NotificationResponse>> GetAllNotifications(string userId)
        {
            // Get user
            var user = await _userRepository.GetSingle(userId);

            // Check if it exists
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get all notifications
            var notifications = await _notificationRepository.GetAll(NotificationExpression.NotificationFilter(userId));

            // Response
            var response = _mapper.Map<List<NotificationResponse>>(notifications);

            // Return
            return response;
        }
        public async Task<NotificationResponse> GetNotification(Guid notificationId)
        {
            // Get notification
            var notification = await _notificationRepository.GetSingle(notificationId);

            // Throw NotFound exception if the currency does not exist
            if (notification == null) throw new NotFoundException(NotificationMessage.NotificationNotFound);

            // Response
            var response = _mapper.Map<NotificationResponse>(notification);

            // Return
            return response;
        }
    }
}
