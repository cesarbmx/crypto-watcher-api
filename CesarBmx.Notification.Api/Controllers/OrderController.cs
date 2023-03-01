using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CesarBmx.Shared.Application.Responses;
using CesarBmx.Notification.Application.Responses;
using CesarBmx.Notification.Application.Services;
using CesarBmx.Shared.Api.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CesarBmx.Notification.Api.Controllers
{
    [SwaggerResponse(500, Type = typeof(InternalServerError))]
    [SwaggerResponse(401, Type = typeof(Unauthorized))]
    [SwaggerResponse(403, Type = typeof(Forbidden))]
    [SwaggerOrder(orderPrefix: "F")]
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
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
        [SwaggerResponse(404, Type = typeof(NotFound))]
        [SwaggerOperation(Tags = new[] { "Orders" }, OperationId = "Orders_GetOrder")]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            // Reponse
            var response = await _orderService.GetOrder(orderId);

            // Return
            return Ok(response);
        }
    }
}

