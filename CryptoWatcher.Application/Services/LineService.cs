using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Repositories;

namespace CryptoWatcher.Application.Services
{
    public class LineService
    {
        private readonly IRepository<Line> _lineRepository;
        private readonly IMapper _mapper;

        public LineService(IRepository<Line> lineRepository, IMapper mapper)
        {
            _lineRepository = lineRepository;
            _mapper = mapper;
        }
        public async Task<List<LineResponse>> GetAllLines(string currencyId = null, string indicatorId = null)
        {
            // Get all lines
            var lines = await _lineRepository.GetAll(LineExpression.Filter(currencyId, indicatorId));

            // Response
            var response = _mapper.Map<List<LineResponse>>(lines);

            // Return
            return response;
        }
    }
}
