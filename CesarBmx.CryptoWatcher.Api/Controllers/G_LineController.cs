﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CesarBmx.CryptoWatcher.Application.Queries;
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
        /// Get lines
        /// </summary>
        [HttpGet]
        [Route("api/lines")]
        [SwaggerResponse(200, Type = typeof(List<Line>))]
        [SwaggerOperation(Tags = new[] { "Lines" }, OperationId = "Lines_GetLines")]
        public async Task<IActionResult> GetLines(GetLines query)
        {
            // Reponse
            var response = await _lineService.GetLines(query.Period, query.CurrencyIds, query.UserIds);

            // Return
            return Ok(response);
        }
    }
}

