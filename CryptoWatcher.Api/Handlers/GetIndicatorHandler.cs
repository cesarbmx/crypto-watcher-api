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
    public class GetIndicatorHandler : IRequestHandler<GetIndicatorRequest, IndicatorResponse>
    {
        private readonly IRepository<Indicator> _indicatorRepository;
        private readonly IMapper _mapper;

        public GetIndicatorHandler(
            IRepository<Indicator> indicatorRepository,
            IMapper mapper)
        {
            _indicatorRepository = indicatorRepository;
            _mapper = mapper;
        }

        public async Task<IndicatorResponse> Handle(GetIndicatorRequest request, CancellationToken cancellationToken)
        {

            // Get indicator
            var indicator = await _indicatorRepository.GetSingle(request.IndicatorId);

            // Throw NotFound exception if it does not exist
            if (indicator == null) throw new NotFoundException(IndicatorMessage.IndicatorNotFound);

            // Response
            var response = _mapper.Map<IndicatorResponse>(indicator);

            // Return
            return response;
        }
    }
}
