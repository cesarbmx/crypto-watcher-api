using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Api.Requests;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Domain;
using CryptoWatcher.Shared.Exceptions;
using MediatR;

namespace CryptoWatcher.Api.Handlers
{
    public class GetNotificationHandler : IRequestHandler<GetNotificationRequest, NotificationResponse>
    {
        private readonly IRepository<Notification> _notificationRepository;
        private readonly IMapper _mapper;

        public GetNotificationHandler(IRepository<Notification> notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        public async Task<NotificationResponse> Handle(GetNotificationRequest request, CancellationToken cancellationToken)
        {
            // Get notification
            var notification = await _notificationRepository.GetSingle(request.NotificationId);

            // Throw NotFound exception if the currency does not exist
            if (notification == null) throw new NotFoundException(NotificationMessage.NotificationNotFound);

            // Response
            var response = _mapper.Map<NotificationResponse>(notification);

            // Return
            return response;
        }
    }
}
