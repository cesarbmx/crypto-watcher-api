using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using CesarBmx.Shared.Application.Exceptions;
using CesarBmx.Shared.Logging.Extensions;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Application.Messages;
using CryptoWatcher.Domain.Models;
using CesarBmx.Shared.Persistence.Repositories;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Persistence.Contexts;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Services
{
    public class OrderService
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Watcher> _watcherRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;

        public OrderService(
            MainDbContext mainDbContext,
            IRepository<Order> orderRepository,
            IRepository<User> userRepository,
            IRepository<Watcher> watcherRepository,
            IMapper mapper,
            ILogger<OrderService> logger)
        {
            _mainDbContext = mainDbContext;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _watcherRepository = watcherRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<OrderResponse>> GetAllOrders(string userId)
        {
            // Get user
            var user = await _userRepository.GetSingle(userId);

            // Check if it exists
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

            // Throw NotFoundException if it does not exist
            if (order == null) throw new NotFoundException(OrderMessage.OrderNotFound);

            // Response
            var response = _mapper.Map<OrderResponse>(order);

            // Return
            return response;
        }
        public async Task UpdateOrders()
        {
            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Time
            var time = DateTime.Now;

            // Get all watchers with buy or sells
            var watchers = await _watcherRepository.GetAll(WatcherExpression.WatcherWillingToBuyOrSell());

            // Get all orders
            var orders = await _orderRepository.GetAll();

            // Build new orders
            var newOrders = OrderBuilder.BuildNewOrders(watchers, orders, time);

            // Add
            _orderRepository.AddRange(newOrders, time);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Stop watch
            stopwatch.Stop();

            // Log into Splunk
            _logger.LogSplunkInformation("UpdateOrders", new
            {
                newOrders.Count,
                ExecutionTime = stopwatch.Elapsed.TotalSeconds
            });
        }
    }
}
