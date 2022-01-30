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
    public class ScriptVariablesService
    {
        private readonly MainDbContext _mainDbContext;

        public ScriptVariablesService(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task<ScriptVariables> GetScriptVariables(GetScriptVariables query)
        {
            // Get all lines
            var lines = await _mainDbContext.Lines.Where(LineExpression.Filter(query.Period, query.CurrencyIds, query.UserIds, query.IndicatorIds)).ToListAsync();

            // Response
            var response = ScriptVariablesBuilder.BuildScriptVariables(lines);

            // Return
            return response;
        }
    }
}
