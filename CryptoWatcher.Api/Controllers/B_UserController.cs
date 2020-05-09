using System.Collections.Generic;
using System.Threading.Tasks;
using CesarBmx.Shared.Application.Responses;
using CryptoWatcher.Application.Requests;
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
    public class B_UsersController : Controller
    {
        private readonly UserService _userService;

        public B_UsersController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        [HttpGet]
        [Route("users")]
        [SwaggerResponse(200, Type = typeof(List<UserResponse>))]
        [SwaggerOperation(Tags = new[] { "Users" }, OperationId = "Users_GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            // Reponse
            var response = await _userService.GetAllUsers();

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get user
        /// </summary>
        [HttpGet]
        [Route("users/{userId}", Name = "Users_GetUser")]
        [SwaggerResponse(200, Type = typeof(UserResponse))]
        [SwaggerResponse(404, Type = typeof(ErrorResponse))]
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
        [Route("users")]
        [SwaggerResponse(201, Type = typeof(UserResponse))]
        [SwaggerResponse(400, Type = typeof(ErrorResponse))]
        [SwaggerResponse(409, Type = typeof(ErrorResponse))]
        [SwaggerResponse(422, Type = typeof(ValidationResponse))]
        [SwaggerOperation(Tags = new[] { "Users" }, OperationId = "Users_AddUser")]
        public async Task<IActionResult> AddUser([FromBody]AddUserRequest request)
        {
            // Reponse
            var response = await _userService.AddUser(request);

            // Return
            return CreatedAtRoute("Users_GetUser", new { response.UserId }, response);
        }
    }
}

