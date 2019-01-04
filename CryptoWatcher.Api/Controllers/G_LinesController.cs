using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Api.ResponseExamples;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Application.Services;
using CryptoWatcher.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.Controllers
{
    // ReSharper disable once InconsistentNaming
    public class G_LinesController : Controller
    {
        private readonly LineService _lineService;

        public G_LinesController(LineService lineService)
        {
            _lineService = lineService;
        }

        /// <summary>
        /// Get all lines
        /// </summary>
        [HttpGet]
        [Route("lines")]
        [SwaggerResponse(200, Type = typeof(List<DataPointResponse>))]       
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(LineListResponseExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Lines" }, OperationId = "Lines_GetAllLines")]
        public async Task<IActionResult> GetAllLines(string currencyId = null, IndicatorType? indicatorType = null, string indicatorId = null, string userId = null)
        {
            // Reponse
            var response = await _lineService.GetAllLines(currencyId, indicatorType, indicatorId, userId);

            // Return
            return Ok(response);
        }
    }
}

