using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Messages;
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
        private readonly ILogger<AddIndicatorRequest> _logger;
        private readonly IMapper _mapper;

        public IndicatorService(
            MainDbContext mainDbContext,
            IRepository<Indicator> indicatorRepository,
            IRepository<User> userRepository,
            ILogger<AddIndicatorRequest> logger,
            IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _indicatorRepository = indicatorRepository;
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
            var indicators = await _indicatorRepository.GetAll(IndicatorExpression.IndicatorFilter(indicatorType, null, userId), x => x.Dependencies);

            // Response
            var response = _mapper.Map<List<IndicatorResponse>>(indicators);

            // Return
            return response;
        }
        public async Task<IndicatorResponse> GetIndicator(string indicatorId)
        {
            // Get indicator
            var indicator = await _indicatorRepository.GetSingle(indicatorId, x => x.Dependencies);

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
            var indicator = await _indicatorRepository.GetSingle(IndicatorExpression.Indicator(request.UserId, request.Name));

            // Throw NotFound exception if it exists
            if (indicator != null) throw new NotFoundException(IndicatorMessage.IndicatorExists);

            // Add
            indicator = new Indicator(
                request.IndicatorType,
                request.IndicatorId,
                request.UserId,
                request.Name,
                request.Description,
                request.Formula,
                IndicatorBuilder.BuildDependencies(request.IndicatorId, request.Dependencies));           
            _indicatorRepository.Add(indicator);
            // Save
            await _mainDbContext.SaveChangesAsync();

            // Log into Splunk
            _logger.LogSplunkInformation(request);

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

            // Update
            indicator.Update(request.Name, request.Description, request.Formula);
            _indicatorRepository.Update(indicator);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Log into Splunk
            _logger.LogSplunkInformation(request);

            // Response
            var response = _mapper.Map<IndicatorResponse>(indicator);

            // Return
            return response;
        }
    }
}
