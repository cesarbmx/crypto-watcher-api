using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Api.Requests;
using CryptoWatcher.Api.ResponseExamples;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Services;
using CryptoWatcher.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.Controllers
{
    // ReSharper disable once InconsistentNaming
    public class E_NotificationsController : Controller
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;
        private readonly NotificationService _notificationService;

        public E_NotificationsController(MainDbContext mainDbContext, IMapper mapper, NotificationService notificationService)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
            _notificationService = notificationService;
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
            // Get notifications
            var notification = await _notificationService.GetNotifications();

            // Response
            var response = _mapper.Map<List<NotificationResponse>>(notification);

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
            // Get notification
            var notification = await _notificationService.GetNotification(id);

            // Response
            var response = _mapper.Map<NotificationResponse>(notification);

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Add notification
        /// </summary>
        [HttpPost]
        [Route("notifications")]
        [SwaggerResponse(201, Type = typeof(NotificationResponse))]
        [SwaggerResponse(400, Type = typeof(ErrorResponse))]
        [SwaggerResponse(409, Type = typeof(ErrorResponse))]
        [SwaggerResponse(422, Type = typeof(ValidationResponse))]
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(201, typeof(NotificationResponseExample))]
        [SwaggerResponseExample(400, typeof(BadRequestExample))]
        [SwaggerResponseExample(409, typeof(ConflictExample))]
        [SwaggerResponseExample(422, typeof(InvalidRequestExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Notifications" }, OperationId = "Notifications_AddNotification")]
        public async Task<IActionResult> AddNotification([FromBody]AddNotificationRequest request)
        {
            // Add notification
            var notification = await _notificationService.AddNotification(request.Id, request.Message);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Response
            var response = _mapper.Map<NotificationResponse>(notification);

            // Return
            return CreatedAtRoute("Notifications_GetNotification", new { response.Id }, response);
        }
    }
}

