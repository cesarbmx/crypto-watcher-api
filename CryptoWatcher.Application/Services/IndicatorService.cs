using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Application.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Persistence.Repositories;
using CryptoWatcher.Shared.Exceptions;
using CryptoWatcher.Shared.Extensions;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Services
{
    public class IndicatorService
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Indicator> _indicatorRepository;
        private readonly IRepository<IndicatorDependency> _indicatorDependencyRepository;
        private readonly ILogger<IndicatorService> _logger;
        private readonly IMapper _mapper;

        public IndicatorService(
            MainDbContext mainDbContext,
            IRepository<Indicator> indicatorRepository,
            Repository<IndicatorDependency> indicatorDependencyRepository,
            IRepository<User> userRepository,
            ILogger<IndicatorService> logger,
            IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _indicatorRepository = indicatorRepository;
            _indicatorDependencyRepository = indicatorDependencyRepository;
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<IndicatorResponse>> GetAllIndicators(string userId, IndicatorType indicatorType)
        {
            // Get user
            var user = await _userRepository.GetSingle(userId);

            // Check if user exists
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get all indicators
            var indicators = await _indicatorRepository.GetAll(IndicatorExpression.IndicatorFilter(indicatorType, null, userId));

            // Get all indicator dependencies
            foreach (var indicator in indicators)
            {
                var dependencies = await _indicatorDependencyRepository.GetAll(IndicatorDependencyExpression.IndicatorDependencyFilter(indicator.IndicatorId, null));
                indicator.SetDependencies(dependencies);
            }

            // Response
            var response = _mapper.Map<List<IndicatorResponse>>(indicators);

            // Return
            return response;
        }
        public async Task<IndicatorResponse> GetIndicator(string indicatorId)
        {
            // Get indicator
            var indicator = await _indicatorRepository.GetSingle(indicatorId);

            // Throw NotFound exception if it does not exist
            if (indicator == null) throw new NotFoundException(IndicatorMessage.IndicatorNotFound);

            // Get indicator dependencies
            var indicatorsDependencies = await _indicatorDependencyRepository.GetAll(IndicatorDependencyExpression.IndicatorDependencyFilter(indicator.IndicatorId, null));
            indicator.SetDependencies(indicatorsDependencies);

            // Response
            var response = _mapper.Map<IndicatorResponse>(indicator);

            // Return
            return response;
        }
        public async Task<IndicatorResponse> AddIndicator(AddIndicatorRequest request)
        {
            // Check if it exists
            var indicator = await _indicatorRepository.GetSingle(IndicatorExpression.Indicator(request.UserId, request.Name));

            // Throw NotFound exception if it exists
            if (indicator != null) throw new ConflictException(IndicatorMessage.IndicatorAlreadyExists);

            // Add dependencies
            var dependencies = IndicatorBuilder.BuildDependencies(request.IndicatorId, request.Dependencies);
            _indicatorDependencyRepository.AddRange(dependencies);

            // Add
            indicator = new Indicator(
                request.IndicatorType,
                request.IndicatorId,
                request.UserId,
                request.Name,
                request.Description,
                request.Formula,
                dependencies);           
            _indicatorRepository.Add(indicator);

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
            var indicator = await _indicatorRepository.GetSingle(request.IndicatorId);

            // Throw NotFound exception if it does not exist
            if (indicator == null) throw new NotFoundException(IndicatorMessage.IndicatorNotFound);

            // Update indicator dependencies
            var dependencies = await _indicatorDependencyRepository.GetAll(IndicatorDependencyExpression.IndicatorDependencyFilter(indicator.IndicatorId, null));
            var newDependencies = IndicatorBuilder.BuildDependencies(request.IndicatorId, request.Dependencies);
            _indicatorDependencyRepository.AddRange(EntityBuilder.BuildEntitiesToAdd(dependencies, newDependencies));
            _indicatorDependencyRepository.UpdateRange(EntityBuilder.BuildEntitiesToUpdate(dependencies, newDependencies));
            _indicatorDependencyRepository.RemoveRange(EntityBuilder.BuildEntitiesToRemove(dependencies, newDependencies));

            // Update indicator
            indicator.SetDependencies(newDependencies);
            indicator.Update(request.Name, request.Description, request.Formula);
            _indicatorRepository.Update(indicator);

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
