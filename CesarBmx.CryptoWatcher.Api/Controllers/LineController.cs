using System.Collections.Generic;
using System.Threading.Tasks;
using CesarBmx.CryptoWatcher.Application.Queries;
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
    [SwaggerControllerOrder(orderPrefix: "D")]
    public class LineController : Controller
    {
        private readonly LineService _lineService;

        public LineController(LineService lineService)
        {
            _lineService = lineService;
        }

        /// <summary>
        /// Get lines
        /// </summary>
        [HttpGet]
        [Route("api/lines")]
        [SwaggerResponse(200, Type = typeof(List<Line>))]
        [SwaggerOperation(Tags = new[] { "Lines" }, OperationId = "Lines_GetLines")]
        public async Task<IActionResult> GetLines(GetLines query)
        {
            // Reponse
            var response = await _lineService.GetLines(query);

            // Return
            return Ok(response);
        }
    }
}

