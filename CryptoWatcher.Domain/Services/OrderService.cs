using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Repositories;
using CryptoWatcher.Shared.Exceptions;

namespace CryptoWatcher.Domain.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly UserService _userService;

        public OrderService(IOrderRepository orderRepository, UserService userService)
        {
            _orderRepository = orderRepository;
            _userService = userService;
        }

        public async Task<List<Order>> GetOrders(string userId)
        {
            // Get user
            var user = await _userService.GetUser(userId);

            // Get user orders
            var userOrders = await _orderRepository.GetByUserId(user.UserId);

            // Return
            return userOrders;
        }
        public async Task<Order> GetOrder(string orderId)
        {
            // Get order
            var order = await _orderRepository.GetByOrderId(orderId);

            // Throw NotFound exception if it does not exist
            if (order == null) throw new NotFoundException(OrderMessages.OrderNotFound);

            // Return
            return order;
        }
        public async Task<Order> AddOrder(string userId, string currencyId, decimal quantity)
        {
            // Add order
            var order = new Order(userId, currencyId, quantity);
            _orderRepository.Add(order);

            // Return
            return await Task.FromResult(order);
        }
    }
}
