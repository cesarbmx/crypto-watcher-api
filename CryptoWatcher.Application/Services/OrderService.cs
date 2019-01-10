using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Application.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Repositories;
using CryptoWatcher.Shared.Exceptions;

namespace CryptoWatcher.Application.Services
{
    public class OrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IRepository<Order> orderRepository,
            IRepository<User> userRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderResponse>> GetAllOrders(string userId)
        {
            // Get user
            var user = await _userRepository.GetSingle(userId);

            // Check if user exists
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get all orders
            var orders = await _orderRepository.GetAll(OrderExpression.OrderFilter(userId));

            // Response
            var response = _mapper.Map<List<OrderResponse>>(orders);

            // Return
            return response;
        }
        public async Task<OrderResponse> GetOrder(Guid orderId)
        {
            // Get order
            var order = await _orderRepository.GetSingle(orderId);

            // Throw NotFound exception if the currency does not exist
            if (order == null) throw new NotFoundException(OrderMessage.OrderNotFound);

            // Response
            var response = _mapper.Map<OrderResponse>(order);

            // Return
            return response;
        }
    }
}
