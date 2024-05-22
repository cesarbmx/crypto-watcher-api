using CesarBmx.Shared.Application.Responses;
using CesarBmx.CryptoWatcher.Application.Responses;
using CesarBmx.CryptoWatcher.Application.Services;
using CesarBmx.Shared.Api.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CesarBmx.CryptoWatcher.Api.Controllers
{
    [SwaggerResponse(500, Type = typeof(InternalServerError))]
    [SwaggerResponse(401, Type = typeof(Unauthorized))]
    [SwaggerResponse(403, Type = typeof(Forbidden))]
    [SwaggerOrder(orderPrefix: "F")]
    public class UserLogController : Controller
    {
        private readonly UserLogService _userLogService;

        public UserLogController(UserLogService userLogService)
        {
            _userLogService = userLogService;
        }

        /// <summary>
        /// Get user logs
        /// </summary>
        [HttpGet]
        [Route("api/users/{userId}/logs")]
        [SwaggerResponse(200, Type = typeof(List<UserLogResponse>))]
        [SwaggerOperation(Tags = new[] { "User logs" }, OperationId = "UsersLogs_GetUserLogs")]
        public async Task<IActionResult> GetUsers(string userId)
        {
            // Reponse
            var response = await _userLogService.GetUserLogs(userId);

            // Return
            return Ok(response);
        }
    }
}

