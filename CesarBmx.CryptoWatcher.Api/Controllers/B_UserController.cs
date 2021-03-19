using System.Collections.Generic;
using System.Threading.Tasks;
using CesarBmx.Shared.Application.Responses;
using CesarBmx.CryptoWatcher.Application.Requests;
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
        [Route("api/users")]
        [SwaggerResponse(200, Type = typeof(List<User>))]
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
        [Route("api/users/{userId}", Name = "Users_GetUser")]
        [SwaggerResponse(200, Type = typeof(User))]
        [SwaggerResponse(404, Type = typeof(Error))]
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
        [SwaggerResponse(400, Type = typeof(Error))]
        [SwaggerResponse(409, Type = typeof(Error))]
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

