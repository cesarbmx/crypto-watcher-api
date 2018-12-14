using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Domain;
using CryptoWatcher.Shared.Exceptions;
using MediatR;

namespace CryptoWatcher.Application.Handlers
{
    public class GetAllIndicatorsHandler : IRequestHandler<GetAllIndicatorsRequest, List<IndicatorResponse>>
    {
        private readonly IRepository<Indicator> _indicatorRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public GetAllIndicatorsHandler(
            IRepository<Indicator> indicatorRepository,
            IRepository<User> userRepository,
            IMapper mapper)
        {
            _indicatorRepository = indicatorRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<List<IndicatorResponse>> Handle(GetAllIndicatorsRequest request, CancellationToken cancellationToken)
        {
            // Get user
            var user = await _userRepository.GetSingle(request.UserId);

            // Check if user exists
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get indicators
            var userIndicators = await _indicatorRepository.GetAll(IndicatorExpression.Filter(request.UserId));

            // Response
            var response = _mapper.Map<List<IndicatorResponse>>(userIndicators);

            // Return
            return response;
        }
    }
}
