using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Types;
using CryptoWatcher.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Application.Services
{
    public class ScriptVariableSetService
    {
        private readonly MainDbContext _mainDbContext;

        public ScriptVariableSetService(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task<ScriptVariableSet> GetScriptVariableSet(Period period = Period.ONE_MINUTE, List<string> currencyIds = null, List<string> indicatorIds = null)
        {
            // Get all lines
            var lines = await _mainDbContext.Lines.Where(LineExpression.Filter(period, currencyIds, indicatorIds)).ToListAsync();

            // Response
            var response = ScriptVariableSetBuilder.BuildScriptVariableSet(lines);

            // Return
            return response;
        }       
    }
}
