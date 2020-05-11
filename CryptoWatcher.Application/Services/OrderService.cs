using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CesarBmx.Shared.Application.Exceptions;
using CesarBmx.Shared.Logging.Extensions;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Application.Messages;
using CryptoWatcher.Domain.Models;
using CesarBmx.Shared.Persistence.Repositories;
using CryptoWatcher.Domain.ModelBuilders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Services
{
    public class OrderService
    {
        private readonly DbContext _dbContext;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;

        public OrderService(
            DbContext dbContext,
            IRepository<Order> orderRepository,
            IRepository<User> userRepository,
            IMapper mapper,
            ILogger<OrderService> logger)
        {
            _dbContext = dbContext;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
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
        public async Task<List<Order>> UpdateOrders(List<Watcher> watchers)
        {
            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Grab watchers willing to buy or sell
             watchers = watchers.Where(WatcherExpression.WatcherWillingToBuyOrSell().Compile()).ToList();

            // Get all orders
            var orders = await _orderRepository.GetAll();

            // Build new orders
            var newOrders = OrderBuilder.BuildNewOrders(watchers, orders);

            // Add
            _orderRepository.AddRange(newOrders);

            // Save
            await _dbContext.SaveChangesAsync();

            // Stop watch
            stopwatch.Stop();

            // Log into Splunk
            _logger.LogSplunkInformation("UpdateOrders", new
            {
                newOrders.Count,
                ExecutionTime = stopwatch.Elapsed.TotalSeconds
            });

            // Return
            return newOrders;
        }
    }
}
