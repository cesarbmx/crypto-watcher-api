using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Api.Requests;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Domain;
using CryptoWatcher.Shared.Exceptions;
using MediatR;

namespace CryptoWatcher.Api.Handlers
{
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersRequest, List<OrderResponse>>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public GetAllOrdersHandler(
            IRepository<Order> orderRepository,
            IRepository<User> userRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderResponse>> Handle(GetAllOrdersRequest request, CancellationToken cancellationToken)
        {
            // Get user
            var user = await _userRepository.GetSingle(request.UserId);

            // Check if user exists
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get orders
            var orders = await _orderRepository.GetAll(OrderExpression.Filter(request.UserId));

            // Response
            var response = _mapper.Map<List<OrderResponse>>(orders);

            // Return
            return response;
        }
    }
}
