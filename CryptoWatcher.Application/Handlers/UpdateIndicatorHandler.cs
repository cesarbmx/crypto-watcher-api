using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Contexts;
using CryptoWatcher.Shared.Exceptions;
using CryptoWatcher.Shared.Extensions;
using CryptoWatcher.Shared.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Handlers
{
    public class UpdateIndicatorHandler : IRequestHandler<UpdateIndicatorRequest, IndicatorResponse>
    {
        private readonly IContext _context;
        private readonly IRepository<Indicator> _indicatorRepository;
        private readonly ILogger<UpdateIndicatorRequest> _logger;
        private readonly IMapper _mapper;

        public UpdateIndicatorHandler(
            IContext context,
            IRepository<Indicator> indicatorRepository,
            ILogger<UpdateIndicatorRequest> logger,
            IMapper mapper)
        {
            _context = context;
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
            await _context.SaveChangesAsync(cancellationToken);

            // Log into Splunk
            _logger.LogSplunkInformation(request);

            // Response
            var response = _mapper.Map<IndicatorResponse>(indicator);

            // Return
            return response;
        }
    }
}
