using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CesarBmx.CryptoWatcher.Domain.Builders;
using CesarBmx.CryptoWatcher.Domain.Expressions;
using CesarBmx.CryptoWatcher.Domain.Models;
using CesarBmx.CryptoWatcher.Domain.Types;
using CesarBmx.CryptoWatcher.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CesarBmx.CryptoWatcher.Application.Services
{
    public class ScriptVariableSetService
    {
        private readonly MainDbContext _mainDbContext;

        public ScriptVariableSetService(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task<ScriptVariableSet> GetScriptVariableSet(Period period = Period.ONE_MINUTE, List<string> currencyIds = null, List<string> userIds = null, List<string> indicatorIds = null)
        {
            // Get all lines
            var lines = await _mainDbContext.Lines.Where(LineExpression.Filter(period, currencyIds, userIds, indicatorIds)).ToListAsync();

            // Response
            var response = ScriptVariableSetBuilder.BuildScriptVariableSet(lines);

            // Return
            return response;
        }
    }
}
