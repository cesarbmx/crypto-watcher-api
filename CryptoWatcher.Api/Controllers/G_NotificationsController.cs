using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Api.Requests;
using CryptoWatcher.Api.ResponseExamples;
using CryptoWatcher.Api.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.Controllers
{
    // ReSharper disable once InconsistentNaming
    public class G_NotificationsController : Controller
    {
        private readonly IMediator _mediator;

        public G_NotificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all notifications
        /// </summary>
        [HttpGet]
        [Route("users/{userId}/notifications")]
        [SwaggerResponse(200, Type = typeof(List<NotificationResponse>))]
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(NotificationListResponseExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Notifications" }, OperationId = "Notifications_GetAllNotifications")]
        public async Task<IActionResult> GetAllNotifications(string userId)
        {
            // Reponse
            var response = await _mediator.Send(new GetAllNotificationsRequest {UserId = userId });

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get notification
        /// </summary>
        [HttpGet]
        [Route("users/{userId}/notifications/{notificationId}", Name = "Notifications_GetNotification")]
        [SwaggerResponse(200, Type = typeof(NotificationResponse))]
        [SwaggerResponse(404, Type = typeof(ErrorResponse))]
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(NotificationResponseExample))]
        [SwaggerResponseExample(404, typeof(NotFoundExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Notifications" }, OperationId = "Notifications_GetNotification")]
        public async Task<IActionResult> GetNotification(string userId, string notificationId)
        {
            // Reponse
            var response = await _mediator.Send(new GetNotificationRequest() { NotificationId = notificationId });

            // Return
            return Ok(response);
        }
    }
}

