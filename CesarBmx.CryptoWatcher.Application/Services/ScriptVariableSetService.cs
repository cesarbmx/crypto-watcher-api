using System.Linq;
using System.Threading.Tasks;
using CesarBmx.CryptoWatcher.Application.Queries;
using CesarBmx.CryptoWatcher.Domain.Builders;
using CesarBmx.CryptoWatcher.Domain.Expressions;
using CesarBmx.CryptoWatcher.Domain.Models;
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

        public async Task<ScriptVariableSet> GetScriptVariableSet(GetScriptVariableSet query)
        {
            // Get all lines
            var lines = await _mainDbContext.Lines.Where(LineExpression.Filter(query.Period, query.CurrencyIds, query.UserIds, query.IndicatorIds)).ToListAsync();

            // Response
            var response = ScriptVariableSetBuilder.BuildScriptVariableSet(lines);

            // Return
            return response;
        }
    }
}
