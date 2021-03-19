using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CesarBmx.Shared.Application.Responses;
using CesarBmx.CryptoWatcher.Application.Resources;
using CesarBmx.CryptoWatcher.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CesarBmx.CryptoWatcher.Api.Controllers
{
    [SwaggerResponse(500, Type = typeof(InternalServerError))]
    [SwaggerResponse(401, Type = typeof(Unauthorized))]
    [SwaggerResponse(403, Type = typeof(Forbidden))]
    // ReSharper disable once InconsistentNaming
    public class F_NotificationController : Controller
    {
        private readonly NotificationService _notificationService;

        public F_NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        /// <summary>
        /// Get user notifications
        /// </summary>
        [HttpGet]
        [Route("api/users/{userId}/notifications")]
        [SwaggerResponse(200, Type = typeof(List<Notification>))]
        [SwaggerOperation(Tags = new[] { "Notifications" }, OperationId = "Notifications_GetUserNotifications")]
        public async Task<IActionResult> GetUserNotifications(string userId)
        {
            // Reponse
            var response = await _notificationService.GetUserNotifications(userId);

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get notification
        /// </summary>
        [HttpGet]
        [Route("api/notifications/{notificationId}", Name = "Notifications_GetNotification")]
        [SwaggerResponse(200, Type = typeof(Notification))]
        [SwaggerResponse(404, Type = typeof(Error))]
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

