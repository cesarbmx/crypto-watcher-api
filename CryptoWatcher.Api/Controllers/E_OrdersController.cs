using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CesarBmx.Shared.Application.Responses;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CryptoWatcher.Api.Controllers
{
    [SwaggerResponse(500, Type = typeof(InternalServerErrorResponse))]
    [SwaggerResponse(401, Type = typeof(UnauthorizedResponse))]
    [SwaggerResponse(403, Type = typeof(ForbiddenResponse))]
    // ReSharper disable once InconsistentNaming
    public class E_OrdersController : Controller
    {
        private readonly OrderService _orderService;

        public E_OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Get all orders
        /// </summary>
        [HttpGet]
        [Route("api/users/{userId}/orders")]
        [SwaggerResponse(200, Type = typeof(List<OrderResponse>))]
        [SwaggerOperation(Tags = new[] { "Orders" }, OperationId = "Orders_GetAllOrders")]
        public async Task<IActionResult> GetAllOrders(string userId)
        {
            // Reponse
            var response = await _orderService.GetAllOrders(userId);

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get order
        /// </summary>
        [HttpGet]
        [Route("api/orders/{orderId}", Name = "Orders_GetOrder")]
        [SwaggerResponse(200, Type = typeof(OrderResponse))]
        [SwaggerResponse(404, Type = typeof(ErrorResponse))]
        [SwaggerOperation(Tags = new[] { "Orders" }, OperationId = "Orders_GetOrder")]
        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            // Reponse
            var response = await _orderService.GetOrder(orderId);

            // Return
            return Ok(response);
        }
    }
}

