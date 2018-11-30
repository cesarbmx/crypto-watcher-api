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
    public class E_NotificationsController : Controller
    {
        private readonly IMediator _mediator;

        public E_NotificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get notifications
        /// </summary>
        [HttpGet]
        [Route("notifications")]
        [SwaggerResponse(200, Type = typeof(List<NotificationResponse>))]
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(NotificationListResponseExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Notifications" }, OperationId = "Notifications_GetNotifications")]
        public async Task<IActionResult> GetNotifications()
        {
            // Reponse
            var response = await _mediator.Send(new GetNotificationsRequest());

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get notification
        /// </summary>
        [HttpGet]
        [Route("notifications/{id}", Name = "Notifications_GetNotification")]
        [SwaggerResponse(200, Type = typeof(NotificationResponse))]
        [SwaggerResponse(404, Type = typeof(ErrorResponse))]
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(NotificationResponseExample))]
        [SwaggerResponseExample(404, typeof(NotFoundExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Notifications" }, OperationId = "Notifications_GetNotification")]
        public async Task<IActionResult> GetNotification(string id)
        {
            // Reponse
            var response = await _mediator.Send(new GetNotificationRequest() { Id = id });

            // Return
            return Ok(response);
        }
    }
}

