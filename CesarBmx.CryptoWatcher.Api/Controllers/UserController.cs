using System.Collections.Generic;
using System.Threading.Tasks;
using CesarBmx.Shared.Application.Responses;
using CesarBmx.CryptoWatcher.Application.Requests;
using CesarBmx.CryptoWatcher.Application.Responses;
using CesarBmx.CryptoWatcher.Application.Services;
using CesarBmx.Shared.Api.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using CesarBmx.CryptoWatcher.Application.ConflictReasons;

namespace CesarBmx.CryptoWatcher.Api.Controllers
{
    [SwaggerResponse(500, Type = typeof(InternalServerError))]
    [SwaggerResponse(401, Type = typeof(Unauthorized))]
    [SwaggerResponse(403, Type = typeof(Forbidden))]
    [SwaggerOrder(orderPrefix: "B")]
    public class UsersController : Controller
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get users
        /// </summary>
        [HttpGet]
        [Route("api/users")]
        [SwaggerResponse(200, Type = typeof(List<User>))]
        [SwaggerOperation(Tags = new[] { "Users" }, OperationId = "Users_GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            // Reponse
            var response = await _userService.GetUsers();

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get user
        /// </summary>
        [HttpGet]
        [Route("api/users/{userId}", Name = "Users_GetUser")]
        [SwaggerResponse(200, Type = typeof(User))]
        [SwaggerResponse(404, Type = typeof(NotFound))]
        [SwaggerOperation(Tags = new[] { "Users" }, OperationId = "Users_GetUser")]
        public async Task<IActionResult> GetUser(string userId)
        {
            // Reponse
            var response = await _userService.GetUser(userId);

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Add user
        /// </summary>
        [HttpPost]
        [Route("api/users")]
        [SwaggerResponse(201, Type = typeof(User))]
        [SwaggerResponse(400, Type = typeof(BadRequest))]
        [SwaggerResponse(409, Type = typeof(Conflict<AddUserConflictReason>))]
        [SwaggerResponse(422, Type = typeof(ValidationFailed))]
        [SwaggerOperation(Tags = new[] { "Users" }, OperationId = "Users_AddUser")]
        public async Task<IActionResult> AddUser([FromBody]AddUser request)
        {
            // Reponse
            var response = await _userService.AddUser(request);

            // Return
            return CreatedAtRoute("Users_GetUser", new { response.UserId }, response);
        }
    }
}

