using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Application.Services
{
    public class LineService
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;

        public LineService(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
        public async Task<List<LineResponse>> GetAllLines(string currencyId = null, IndicatorType? indicatorType = null, string indicatorId = null, string userId = null)
        {
            // Get all lines
            var lines = await _mainDbContext.Lines.Where(LineExpression.LineFilter(currencyId, indicatorType, indicatorId, userId)).ToListAsync();

            // Response
            var response = _mapper.Map<List<LineResponse>>(lines);

            // Return
            return response;
        }
    }
}
