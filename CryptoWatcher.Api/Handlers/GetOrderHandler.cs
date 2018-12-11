using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Api.Requests;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Domain;
using CryptoWatcher.Shared.Exceptions;
using MediatR;

namespace CryptoWatcher.Api.Handlers
{
    public class GetOrderHandler : IRequestHandler<GetOrderRequest, OrderResponse>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderHandler(IRepository<Order> orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderResponse> Handle(GetOrderRequest request, CancellationToken cancellationToken)
        {
            // Get order
            var order = await _orderRepository.GetSingle(request.OrderId);

            // Throw NotFound exception if the currency does not exist
            if (order == null) throw new NotFoundException(OrderMessage.OrderNotFound);

            // Response
            var response = _mapper.Map<OrderResponse>(order);

            // Return
            return response;
        }
    }
}
