using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Api.Requests;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Repositories;
using CryptoWatcher.Shared.Exceptions;
using MediatR;

namespace CryptoWatcher.Api.Handlers
{
    public class GetUserNotificationsHandler : IRequestHandler<GetUserNotificationsRequest, List<NotificationResponse>>
    {
        private readonly IRepository<Notification> _notificationRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public GetUserNotificationsHandler(
            IRepository<Notification> notificationRepository,
            IRepository<User> userRepository,
            IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<NotificationResponse>> Handle(GetUserNotificationsRequest request, CancellationToken cancellationToken)
        {
            // Get user
            var user = await _userRepository.GetById(request.Id);

            // Check if user exists
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get user notifications
            var notifications = await _notificationRepository.Get(NotificationExpression.UserNotification(request.Id));

            // Response
            var response = _mapper.Map<List<NotificationResponse>>(notifications);

            // Return
            return response;
        }
    }
}
