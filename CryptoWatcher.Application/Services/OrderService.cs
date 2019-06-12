using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Application.Messages;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Application.Services
{
    public class OrderService
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;

        public OrderService(
            MainDbContext mainDbContext,
            IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }

        public async Task<List<OrderResponse>> GetAllOrders(string userId)
        {
            // Get user
            var user = await _mainDbContext.Users.FindAsync(userId);

            // Check if it exists
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get all orders
            var orders = await _mainDbContext.Orders.Where(OrderExpression.OrderFilter(userId)).ToListAsync();

            // Response
            var response = _mapper.Map<List<OrderResponse>>(orders);

            // Return
            return response;
        }
        public async Task<OrderResponse> GetOrder(Guid orderId)
        {
            // Get order
            var order = await _mainDbContext.Orders.FindAsync(orderId);

            // Throw NotFoundException if it does not exist
            if (order == null) throw new NotFoundException(OrderMessage.OrderNotFound);

            // Response
            var response = _mapper.Map<OrderResponse>(order);

            // Return
            return response;
        }
    }
}
