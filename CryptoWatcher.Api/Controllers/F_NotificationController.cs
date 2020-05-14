using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CesarBmx.Shared.Application.Responses;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CryptoWatcher.Api.Controllers
{
    [SwaggerResponse(500, Type = typeof(InternalServerErrorResponse))]
    [SwaggerResponse(401, Type = typeof(UnauthorizedResponse))]
    [SwaggerResponse(403, Type = typeof(ForbiddenResponse))]
    // ReSharper disable once InconsistentNaming
    public class F_NotificationController : Controller
    {
        private readonly NotificationService _notificationService;

        public F_NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        /// <summary>
        /// Get all notifications
        /// </summary>
        [HttpGet]
        [Route("api/users/{userId}/notifications")]
        [SwaggerResponse(200, Type = typeof(List<NotificationResponse>))]
        [SwaggerOperation(Tags = new[] { "Notifications" }, OperationId = "Notifications_GetAllNotifications")]
        public async Task<IActionResult> GetAllNotifications(string userId)
        {
            // Reponse
            var response = await _notificationService.GetAllNotifications(userId);

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get notification
        /// </summary>
        [HttpGet]
        [Route("api/notifications/{notificationId}", Name = "Notifications_GetNotification")]
        [SwaggerResponse(200, Type = typeof(NotificationResponse))]
        [SwaggerResponse(404, Type = typeof(ErrorResponse))]
        [SwaggerOperation(Tags = new[] { "Notifications" }, OperationId = "Notifications_GetNotification")]
        public async Task<IActionResult> GetNotification(Guid notificationId)
        {
            // Reponse
            var response = await _notificationService.GetNotification(notificationId);

            // Return
            return Ok(response);
        }
    }
}

