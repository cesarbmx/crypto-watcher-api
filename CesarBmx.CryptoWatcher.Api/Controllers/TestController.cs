using CesarBmx.CryptoWatcher.Application.Requests;
using CesarBmx.CryptoWatcher.Application.Services;
using CesarBmx.Shared.Api.ActionFilters;
using CesarBmx.Shared.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CesarBmx.CryptoWatcher.Api.Controllers
{
    [SwaggerResponse(500, Type = typeof(InternalServerError))]
    [SwaggerResponse(400, Type = typeof(BadRequest))]
    [SwaggerResponse(401, Type = typeof(Unauthorized))]
    [SwaggerOrder(orderPrefix: "A")]
    public class TestController : Controller
    {
        private readonly TestService _testService;

        public TestController(TestService testService)
        {
            _testService = testService;
        }


        /// <summary>
        /// Test logging
        /// </summary>
        [HttpPost]
        [Route("test/log")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400, Type = typeof(BadRequest))]
        [SwaggerResponse(422, Type = typeof(Validation))]
        [SwaggerOperation(Tags = new[] { "Test (It will be removed)" }, OperationId = "Test_Log")]
        [ServiceFilter(typeof(LogExecutionTimeAttribute))]
        public async Task<IActionResult> TestLog([FromBody] TestLogging request)
        {
            await _testService.TestLogging(request);

            // Return
            return Ok();
        }

        /// <summary>
        /// Test logging with exception
        /// </summary>
        [HttpPost]
        [Route("test/log-with-exception")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400, Type = typeof(BadRequest))]
        [SwaggerResponse(422, Type = typeof(Validation))]
        [SwaggerOperation(Tags = new[] { "Test (It will be removed)" }, OperationId = "Test_Logging")]
        [ServiceFilter(typeof(LogExecutionTimeAttribute))]
        public IActionResult TestLogException([FromBody] TestLogging request)
        {
            throw new Exception("My exception");
        }
    }
}
