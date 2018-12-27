using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Api.ResponseExamples;
using CryptoWatcher.Application.Orders.Requests;
using CryptoWatcher.Application.Orders.Responses;
using CryptoWatcher.Application.System.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.Controllers
{
    // ReSharper disable once InconsistentNaming
    public class F_OrdersController : Controller
    {
        private readonly IMediator _mediator;

        public F_OrdersController(IMediator mediator)
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
            // Request
            var request = new GetAllOrdersRequest { UserId = userId };

            // Reponse
            var response = await _mediator.Send(request);

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get order
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
        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            // Request
            var request = new GetOrderRequest { OrderId = orderId };

            // Reponse
            var response = await _mediator.Send(request);

            // Return
            return Ok(response);
        }
    }
}

