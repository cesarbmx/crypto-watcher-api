using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Api.Requests;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Repositories;
using CryptoWatcher.Shared.Exceptions;
using MediatR;

namespace CryptoWatcher.Api.Handlers
{
    public class GetOrdersHandler : IRequestHandler<GetUserOrdersRequest, List<OrderResponse>>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public GetOrdersHandler(
            IRepository<Order> orderRepository,
            IRepository<User> userRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderResponse>> Handle(GetUserOrdersRequest request, CancellationToken cancellationToken)
        {
            // Get user
            var user = await _userRepository.GetById(request.Id);

            // Check if user exists
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get user orders
            var orders = await _orderRepository.Get(OrderExpression.UserOrder(request.Id));

            // Response
            var response = _mapper.Map<List<OrderResponse>>(orders);

            // Return
            return response;
        }
    }
}
