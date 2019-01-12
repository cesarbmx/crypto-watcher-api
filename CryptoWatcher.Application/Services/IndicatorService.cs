using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Application.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Exceptions;
using CryptoWatcher.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Services
{
    public class IndicatorService
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<IndicatorService> _logger;
        private readonly IMapper _mapper;

        public IndicatorService(
            MainDbContext mainDbContext,
            ILogger<IndicatorService> logger,
            IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<IndicatorResponse>> GetAllIndicators(string userId, IndicatorType indicatorType)
        {
            // Get user
            var user = await _mainDbContext.Users.FindAsync(userId);

            // Check if it exists
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get all indicators
            var expression = IndicatorExpression.IndicatorFilter(indicatorType, null, userId);
            var indicators = await _mainDbContext.Indicators.Where(expression).ToListAsync();

            // Response
            var response = _mapper.Map<List<IndicatorResponse>>(indicators);

            // Return
            return response;
        }
        public async Task<IndicatorResponse> GetIndicator(string indicatorId)
        {
            // Get indicator
            var expression = IndicatorExpression.Indicator(indicatorId);
            var indicator = await _mainDbContext.Indicators
                .Include(x => x.Dependencies)
                .SingleOrDefaultAsync(expression);

            // Throw NotFound exception if it does not exist
            if (indicator == null) throw new NotFoundException(IndicatorMessage.IndicatorNotFound);

            // Response
            var response = _mapper.Map<IndicatorResponse>(indicator);

            // Return
            return response;
        }
        public async Task<IndicatorResponse> AddIndicator(AddIndicatorRequest request)
        {
            // Check if it exists
            var expression = IndicatorExpression.Indicator(request.UserId, request.Name);
            var indicator = await _mainDbContext.Indicators.SingleOrDefaultAsync(expression);

            // Throw ConflictException exception if it exists
            if (indicator != null) throw new ConflictException(IndicatorMessage.IndicatorAlreadyExists);

            // Add
            var dependencies = IndicatorDependencyBuilder.BuildDependencies(request.IndicatorId, request.Dependencies);
            indicator = new Indicator(
                request.IndicatorType,
                request.IndicatorId,
                request.UserId,
                request.Name,
                request.Description,
                request.Formula,
                dependencies);
            _mainDbContext.Indicators.Add(indicator);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Log into Splunk
            _logger.LogSplunkRequest(request);

            // Response
            var response = _mapper.Map<IndicatorResponse>(indicator);

            // Return
            return response;
        }
        public async Task<IndicatorResponse> UpdateIndicator(UpdateIndicatorRequest request)
        {
            // Get indicator
            var expression = IndicatorExpression.Indicator(request.IndicatorId);
            var indicator = await _mainDbContext.Indicators
                .Include(x => x.Dependencies)
                .SingleOrDefaultAsync(expression);

            // Throw NotFound exception if it does not exist
            if (indicator == null) throw new NotFoundException(IndicatorMessage.IndicatorNotFound);

            // Update dependencies
            var dependencies = indicator.Dependencies;
            var newDependencies = IndicatorDependencyBuilder.BuildDependencies(request.IndicatorId, request.Dependencies);
            _mainDbContext.IndicatorDependencies.RemoveRange(EntityBuilder.BuildEntitiesToRemove(dependencies, newDependencies));
            _mainDbContext.IndicatorDependencies.AddRange(EntityBuilder.BuildEntitiesToAdd(dependencies, newDependencies));
            _mainDbContext.IndicatorDependencies.UpdateRange(EntityBuilder.BuildEntitiesToUpdate(dependencies, newDependencies));

            // Update indicator
            indicator.Update(request.Name, request.Description, request.Formula, dependencies);
            _mainDbContext.Indicators.Update(indicator);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Log into Splunk
            _logger.LogSplunkRequest(request);

            // Response
            var response = _mapper.Map<IndicatorResponse>(indicator);

            // Return
            return response;
        }
    }
}
