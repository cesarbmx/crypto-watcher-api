using System.Collections.Generic;
using System.Threading.Tasks;
using CesarBmx.Shared.Application.Responses;
using CesarBmx.CryptoWatcher.Application.Responses;
using CesarBmx.CryptoWatcher.Application.Services;
using CesarBmx.CryptoWatcher.Domain.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace CesarBmx.CryptoWatcher.Api.Controllers
{
    [SwaggerResponse(500, Type = typeof(InternalServerError))]
    [SwaggerResponse(401, Type = typeof(Unauthorized))]
    [SwaggerResponse(403, Type = typeof(Forbidden))]
    // ReSharper disable once InconsistentNaming
    public class G_LineController : Controller
    {
        private readonly LineService _lineService;

        public G_LineController(LineService lineService)
        {
            _lineService = lineService;
        }

        /// <summary>
        /// Get all lines
        /// </summary>
        [HttpGet]
        [Route("api/lines")]
        [SwaggerResponse(200, Type = typeof(List<Line>))]
        [SwaggerOperation(Tags = new[] { "Lines" }, OperationId = "Lines_GetAllLines")]
        public async Task<IActionResult> GetAllLines([BindRequired] Period period = Period.ONE_MINUTE, List<string> currencyIds = null, List<string> userIds = null, List<string> indicatorIds = null)
        {
            // Reponse
            var response = await _lineService.GetAllLines(period, currencyIds, userIds, indicatorIds);

            // Return
            return Ok(response);
        }
    }
}

