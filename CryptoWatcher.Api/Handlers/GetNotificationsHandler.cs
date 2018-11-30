using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Api.Requests;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Repositories;
using MediatR;

namespace CryptoWatcher.Api.Handlers
{
    public class GetNotificationsHandler : IRequestHandler<GetNotificationsRequest, List<NotificationResponse>>
    {
        private readonly IRepository<Notification> _notificationRepository;
        private readonly IMapper _mapper;

        public GetNotificationsHandler(IRepository<Notification> notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        public async Task<List<NotificationResponse>> Handle(GetNotificationsRequest request, CancellationToken cancellationToken)
        {
            // Get notifications
            var notifications = await _notificationRepository.GetAll();

            // Response
            var response = _mapper.Map<List<NotificationResponse>>(notifications);

            // Return
            return response;
        }
    }
}
