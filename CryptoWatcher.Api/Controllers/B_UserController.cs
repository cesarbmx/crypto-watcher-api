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
    public class B_UsersController : Controller
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;
        private readonly UserService _userService;

        public B_UsersController(MainDbContext mainDbContext, IMapper mapper, UserService userService)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
            _userService = userService;
        }

        /// <summary>
        /// Get user users
        /// </summary>
        [HttpGet]
        [Route("users/{userId}/users")]
        [SwaggerResponse(200, Type = typeof(List<UserResponse>))]       
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(UserListResponseExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Users" }, OperationId = "Users_GetUserUsers")]
        public async Task<IActionResult> GetUsers(string userId)
        {
            // Get user
            var user = await _userService.GetUsers(userId);

            // Response
            var response = _mapper.Map<List<UserResponse>>(user);

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
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(UserResponseExample))]
        [SwaggerResponseExample(404, typeof(NotFoundExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Users" }, OperationId = "Users_GetUser")]
        public async Task<IActionResult> GetUser(string userId)
        {
            // Get user
            var user = await _userService.GetUser(userId);

            // Response
            var response = _mapper.Map<UserResponse>(user);

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
        [SwaggerResponse(404, Type = typeof(ErrorResponse))]
        [SwaggerResponse(409, Type = typeof(ErrorResponse))]
        [SwaggerResponse(422, Type = typeof(ValidationResponse))]
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(201, typeof(UserResponseExample))]
        [SwaggerResponseExample(400, typeof(BadRequestExample))]
        [SwaggerResponseExample(404, typeof(NotFoundExample))]
        [SwaggerResponseExample(409, typeof(ConflictExample))]
        [SwaggerResponseExample(422, typeof(InvalidRequestExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Users" }, OperationId = "Users_AddUser")]
        public async Task<IActionResult> AddUser([FromBody]AddUserRequest request)
        {
            // Add user
            var userSettings = await _userService.AddUser(request.UserId);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Response
            var response = userSettings;

            // Return
            return CreatedAtRoute("Users_GetUser", new { response.UserId }, response);
        }
    }
}

