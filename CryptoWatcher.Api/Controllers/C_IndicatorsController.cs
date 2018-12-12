using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Api.RequestExamples;
using CryptoWatcher.Api.Requests;
using CryptoWatcher.Api.ResponseExamples;
using CryptoWatcher.Api.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.Controllers
{
    // ReSharper disable once InconsistentNaming
    public class C_IndicatorsController : Controller
    {
        private readonly IMediator _mediator;

        public C_IndicatorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all indicators
        /// </summary>
        [HttpGet]
        [Route("users/{userId}/indicators")]
        [SwaggerResponse(200, Type = typeof(List<IndicatorResponse>))]       
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(IndicatorListResponseExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Indicators" }, OperationId = "Indicators_GetAllIndicators")]
        public async Task<IActionResult> GetAllIndicators(string userId)
        {
            // Reponse
            var response = await _mediator.Send(new GetAllIndicatorsRequest{UserId = userId});

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get indicator
        /// </summary>
        [HttpGet]
        [Route("indicators/{indicatorId}", Name = "Indicators_GetIndicator")]
        [SwaggerResponse(200, Type = typeof(IndicatorResponse))]
        [SwaggerResponse(404, Type = typeof(ErrorResponse))]
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(IndicatorResponseExample))]
        [SwaggerResponseExample(404, typeof(NotFoundExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Indicators" }, OperationId = "Indicators_GetIndicator")]
        public async Task<IActionResult> GetIndicator(string indicatorId)
        {
            // Reponse
            var response = await _mediator.Send(new GetIndicatorRequest { IndicatorId = indicatorId });

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Add indicator
        /// </summary>
        [HttpPost]
        [Route("indicators")]
        [SwaggerResponse(201, Type = typeof(IndicatorResponse))]
        [SwaggerResponse(400, Type = typeof(ErrorResponse))]
        [SwaggerResponse(404, Type = typeof(ErrorResponse))]
        [SwaggerResponse(409, Type = typeof(ErrorResponse))]
        [SwaggerResponse(422, Type = typeof(ValidationResponse))]
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(201, typeof(IndicatorResponseExample))]
        [SwaggerResponseExample(400, typeof(BadRequestExample))]
        [SwaggerResponseExample(404, typeof(NotFoundExample))]
        [SwaggerResponseExample(409, typeof(ConflictExample))]
        [SwaggerResponseExample(422, typeof(InvalidRequestExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerRequestExample(typeof(AddIndicatorRequest), typeof(AddIndicatorRequestExample))]
        [SwaggerOperation(Tags = new[] { "Indicators" }, OperationId = "Indicators_AddIndicator")]
        public async Task<IActionResult> AddIndicator([FromBody]AddIndicatorRequest request)
        {
            // Reponse
            var response = await _mediator.Send(request);

            // Return
            return CreatedAtRoute("Indicators_GetIndicator", new { response.IndicatorId }, response);
        }

        /// <summary>
        /// Update indicator
        /// </summary>
        [HttpPut]
        [Route("indicators/{indicatorId}")]
        [SwaggerResponse(200, Type = typeof(IndicatorResponse))]
        [SwaggerResponse(400, Type = typeof(ErrorResponse))]
        [SwaggerResponse(409, Type = typeof(ErrorResponse))]
        [SwaggerResponse(422, Type = typeof(ValidationResponse))]
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(IndicatorResponseExample))]
        [SwaggerResponseExample(400, typeof(BadRequestExample))]
        [SwaggerResponseExample(409, typeof(ConflictExample))]
        [SwaggerResponseExample(422, typeof(InvalidRequestExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerRequestExample(typeof(UpdateIndicatorRequest), typeof(UpdateIndicatorRequestExample))]
        [SwaggerOperation(Tags = new[] { "Indicators" }, OperationId = "Indicators_UpdateIndicator")]
        public async Task<IActionResult> UpdateIndicator(string indicatorId, [FromBody]UpdateIndicatorRequest request)
        {
            // Reponse
            request.IndicatorId = indicatorId;
            var response = await _mediator.Send(request);

            // Return
            return Ok(response);
        }
    }
}

