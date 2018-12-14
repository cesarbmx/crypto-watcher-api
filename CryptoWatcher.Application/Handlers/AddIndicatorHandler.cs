using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Expressions;
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
    public class AddIndicatorHandler : IRequestHandler<AddIndicatorRequest, IndicatorResponse>
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IRepository<Indicator> _indicatorRepository;
        private readonly ILogger<AddIndicatorRequest> _logger;
        private readonly IMapper _mapper;

        public AddIndicatorHandler(
            MainDbContext mainDbContext,
            IRepository<Indicator> indicatorRepository,
            ILogger<AddIndicatorRequest> logger,
            IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _indicatorRepository = indicatorRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IndicatorResponse> Handle(AddIndicatorRequest request, CancellationToken cancellationToken)
        {
            // Check if it exists
            var indicator = await _indicatorRepository.GetSingle(IndicatorExpression.Indicator(request.UserId, request.Name));

            // Throw NotFound exception if it exists
            if (indicator != null) throw new NotFoundException(IndicatorMessage.IndicatorExists);

            // Add
            indicator = new Indicator(
                request.UserId, 
                request.Name, 
                request.Description,
                request.Formula);
            _indicatorRepository.Add(indicator);

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
