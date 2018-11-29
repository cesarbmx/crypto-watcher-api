using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Repositories;
using CryptoWatcher.Shared.Exceptions;

namespace CryptoWatcher.Domain.Services
{
    public class OrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly UserService _userService;
        private readonly WatcherService _watcherService;

        public OrderService(
            IRepository<Order> orderRepository,
            UserService userService,
            WatcherService watcherService)
        {
            _orderRepository = orderRepository;
            _userService = userService;
            _watcherService = watcherService;
        }

        public async Task<List<Order>> GetUserOrders(string userId)
        {
            // Get user
            var user = await _userService.GetUser(userId);

            // Get user orders
            var userOrders = await _orderRepository.Get(OrderExpression.UserOrder(user.Id));

            // Return
            return userOrders;
        }
        public async Task<Order> GetOrder(string id)
        {
            // Get order
            var order = await _orderRepository.GetById(id);

            // Throw NotFound exception if it does not exist
            if (order == null) throw new NotFoundException(OrderMessage.OrderNotFound);

            // Return
            return order;
        }
        public async Task<Order> AddOrder(string userId, string currencyId, OrderType type, decimal quantity)
        {
            // Add order
            var order = new Order(userId, currencyId, type, quantity);
            _orderRepository.Add(order);

            // Return
            return await Task.FromResult(order);
        }
        public async Task AddOrdersFromWatchers()
        {
            // Get watchers willing to buy
            var watchersBuys = await _watcherService.GetWatchersWillingToBuy();
            foreach (var watcher in watchersBuys)
            {
                // Get ongoing orders
                var orders = await _orderRepository.Get(OrderExpression.UserOrder(watcher.UserId, watcher.CurrencyId));

                // if there are no orders yet or the one that exists is also a buy order, then we place it
                if (orders.Count == 0 || orders[0].Type == OrderType.BuyLimit)
                {
                    var order = new Order(watcher.UserId, watcher.Id, OrderType.BuyLimit, 100);
                    _orderRepository.Add(order);
                }
            }

            // Get watchers willing to sell
            var watchersSells = await _watcherService.GetWatchersWillingToSell();
            foreach (var watcher in watchersSells)
            {
                // Get ongoing orders
                var orders = await _orderRepository.Get(OrderExpression.UserOrder(watcher.UserId, watcher.CurrencyId));

                // if there are no orders yet or the one that exists is also a sell order, then we place it
                if (orders.Count == 0 || orders[0].Type == OrderType.BuyLimit)
                {
                    var order = new Order( watcher.UserId, watcher.Id, OrderType.SellMarket, 100);
                    _orderRepository.Add(order);
                }
            }
        }
    }
}
