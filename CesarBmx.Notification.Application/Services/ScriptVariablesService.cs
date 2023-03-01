using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CesarBmx.Notification.Domain.Builders;
using CesarBmx.Notification.Domain.Expressions;
using CesarBmx.Notification.Domain.Models;
using CesarBmx.Notification.Domain.Types;
using CesarBmx.Notification.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CesarBmx.Notification.Application.Services
{
    public class ScriptVariablesService
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ActivitySource _activitySource;

        public ScriptVariablesService(MainDbContext mainDbContext, ActivitySource activitySource)
        {
            _mainDbContext = mainDbContext;
            _activitySource = activitySource;
        }

        public async Task<ScriptVariables> GetScriptVariables(LineRetention lineRetention, Period period, List<string> currencyIds, List<string> userIds, List<string> indicatorIds)
        {
            // Start span
            using var span = _activitySource.StartActivity(nameof(GetScriptVariables));

            // Get all lines
            var lines = await _mainDbContext.Lines.Where(LineExpression.Filter(lineRetention, period, currencyIds, userIds, indicatorIds)).ToListAsync();

            // Response
            var response = ScriptVariablesBuilder.BuildScriptVariables(lines);

            // Return
            return response;
        }
    }
}
