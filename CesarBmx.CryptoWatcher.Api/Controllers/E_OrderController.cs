using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CesarBmx.Shared.Application.Responses;
using CesarBmx.CryptoWatcher.Application.Responses;
using CesarBmx.CryptoWatcher.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CesarBmx.CryptoWatcher.Api.Controllers
{
    [SwaggerResponse(500, Type = typeof(InternalServerError))]
    [SwaggerResponse(401, Type = typeof(Unauthorized))]
    [SwaggerResponse(403, Type = typeof(Forbidden))]
    // ReSharper disable once InconsistentNaming
    public class E_OrderController : Controller
    {
        private readonly OrderService _orderService;

        public E_OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Get user orders
        /// </summary>
        [HttpGet]
        [Route("api/users/{userId}/orders")]
        [SwaggerResponse(200, Type = typeof(List<Order>))]
        [SwaggerOperation(Tags = new[] { "Orders" }, OperationId = "Orders_GetUserOrders")]
        public async Task<IActionResult> GetUserOrders(string userId)
        {
            // Reponse
            var response = await _orderService.GetUserOrders(userId);

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get order
        /// </summary>
        [HttpGet]
        [Route("api/orders/{orderId}", Name = "Orders_GetOrder")]
        [SwaggerResponse(200, Type = typeof(Order))]
        [SwaggerResponse(404, Type = typeof(Error))]
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

