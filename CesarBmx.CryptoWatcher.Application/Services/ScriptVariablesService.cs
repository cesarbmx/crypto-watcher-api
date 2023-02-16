using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CesarBmx.CryptoWatcher.Domain.Builders;
using CesarBmx.CryptoWatcher.Domain.Expressions;
using CesarBmx.CryptoWatcher.Domain.Models;
using CesarBmx.CryptoWatcher.Domain.Types;
using CesarBmx.CryptoWatcher.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Trace;

namespace CesarBmx.CryptoWatcher.Application.Services
{
    public class ScriptVariablesService
    {
        private readonly MainDbContext _mainDbContext;
        private readonly Tracer _tracer;

        public ScriptVariablesService(MainDbContext mainDbContext, Tracer tracer)
        {
            _mainDbContext = mainDbContext;
            _tracer = tracer;
        }

        public async Task<ScriptVariables> GetScriptVariables(LineRetention lineRetention, Period period, List<string> currencyIds, List<string> userIds, List<string> indicatorIds)
        {
            // Start span
            using var span = _tracer.StartActiveSpan(nameof(GetScriptVariables));

            // Get all lines
            var lines = await _mainDbContext.Lines.Where(LineExpression.Filter(lineRetention, period, currencyIds, userIds, indicatorIds)).ToListAsync();

            // Response
            var response = ScriptVariablesBuilder.BuildScriptVariables(lines);

            // Return
            return response;
        }
    }
}
