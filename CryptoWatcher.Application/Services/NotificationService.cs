using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Application.Messages;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Application.Services
{
    public class NotificationService
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;

        public NotificationService(
            MainDbContext mainDbContext,
            IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }

        public async Task<List<NotificationResponse>> GetAllNotifications(string userId)
        {
            // Get user
            var user = await _mainDbContext.Users.FindAsync(userId);

            // Check if it exists
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get all notifications
            var notifications = await _mainDbContext.Notifications.Where(NotificationExpression.NotificationFilter(userId)).ToListAsync();

            // Response
            var response = _mapper.Map<List<NotificationResponse>>(notifications);

            // Return
            return response;
        }
        public async Task<NotificationResponse> GetNotification(Guid notificationId)
        {
            // Get notification
            var notification = await _mainDbContext.Notifications.FindAsync(notificationId);

            // Throw NotFoundException if the currency does not exist
            if (notification == null) throw new NotFoundException(NotificationMessage.NotificationNotFound);

            // Response
            var response = _mapper.Map<NotificationResponse>(notification);

            // Return
            return response;
        }
    }
}
