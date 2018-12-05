using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class D_OrderController : Controller
    {
        private readonly IMediator _mediator;

        public D_OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all orders
        /// </summary>
        [HttpGet]
        [Route("users/{userId}/orders")]
        [SwaggerResponse(200, Type = typeof(List<OrderResponse>))]
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(OrderListResponseExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Orders" }, OperationId = "Orders_GetAllOrders")]
        public async Task<IActionResult> GetAllOrders(string userId)
        {
            // Reponse
            var response = await _mediator.Send(new GetAllOrdersRequest { UserId = userId });

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get an order
        /// </summary>
        [HttpGet]
        [Route("orders/{orderId}", Name = "Orders_GetOrder")]
        [SwaggerResponse(200, Type = typeof(OrderResponse))]
        [SwaggerResponse(404, Type = typeof(ErrorResponse))]
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(OrderResponseExample))]
        [SwaggerResponseExample(404, typeof(NotFoundExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Orders" }, OperationId = "Orders_GetOrder")]
        public async Task<IActionResult> GetOrder(string orderId)
        {
            // Reponse
            var response = await _mediator.Send(new GetOrderRequest { OrderId = orderId });

            // Return
            return Ok(response);
        }
    }
}

