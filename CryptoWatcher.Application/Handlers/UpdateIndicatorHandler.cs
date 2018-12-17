using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Domain;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Exceptions;
using CryptoWatcher.Shared.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Handlers
{
    public class UpdateIndicatorHandler : IRequestHandler<UpdateIndicatorRequest, IndicatorResponse>
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IRepository<Indicator> _indicatorRepository;
        private readonly ILogger<UpdateIndicatorRequest> _logger;
        private readonly IMapper _mapper;

        public UpdateIndicatorHandler(
            MainDbContext mainDbContext,
            IRepository<Indicator> indicatorRepository,
            ILogger<UpdateIndicatorRequest> logger,
            IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _indicatorRepository = indicatorRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IndicatorResponse> Handle(UpdateIndicatorRequest request, CancellationToken cancellationToken)
        {
            // Get indicator
            var indicator = await _indicatorRepository.GetSingle(request.IndicatorId);

            // Throw NotFound exception if it does not exist
            if (indicator == null) throw new NotFoundException(IndicatorMessage.IndicatorNotFound);

            // Update
            indicator.Update(request.Name, request.Description, request.Formula);
            _indicatorRepository.Update(indicator);

            // Save
            await _mainDbContext.SaveChangesAsync(cancellationToken);

            // Log into Splunk
            _logger.LogSplunkInformation(request);

            // Response
            var response = _mapper.Map<IndicatorResponse>(indicator);

            // Return
            return response;
        }
    }
}
