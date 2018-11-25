using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Api.ResponseExamples;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.Controllers
{
    // ReSharper disable once InconsistentNaming
    public class E_OrderController : Controller
    {
        private readonly IMapper _mapper;
        private readonly OrderService _orderService;

        public E_OrderController(IMapper mapper, OrderService orderService)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        /// <summary>
        /// Get user orders
        /// </summary>
        [HttpGet]
        [Route("users/{userId}/orders")]
        [SwaggerResponse(200, Type = typeof(List<OrderResponse>))]
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(OrderListResponseExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Orders" }, OperationId = "Orders_GetUserOrders")]
        public async Task<IActionResult> GetOrders(string userId)
        {
            // Get orders
            var order = await _orderService.GetOrders(userId);

            // Response
            var response = _mapper.Map<List<OrderResponse>>(order);

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
        public async Task<IActionResult> GetOrder(string orderId)
        {
            // Get order
            var order = await _orderService.GetOrder(orderId);

            // Response
            var response = _mapper.Map<OrderResponse>(order);

            // Return
            return Ok(response);
        }
    }
}

