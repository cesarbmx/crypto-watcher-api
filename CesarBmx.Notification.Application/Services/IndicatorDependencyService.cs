using System.Collections.Generic;
using System.Threading.Tasks;
using CesarBmx.Shared.Application.Exceptions;
using CesarBmx.Notification.Application.Messages;
using CesarBmx.Notification.Domain.Builders;
using CesarBmx.Notification.Domain.Models;
using CesarBmx.Notification.Persistence.Contexts;

namespace CesarBmx.Notification.Application.Services
{
    public class IndicatorDependencyService
    {
        private readonly MainDbContext _mainDbContext;

        public IndicatorDependencyService(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task<List<Indicator>> GetDependencies(List<string> dependencyIds)
        {
            var dependencies = new List<Indicator>();
            foreach (var dependencyId in dependencyIds)
            {
                var userId = IndicatorBuilder.BuildUserId(dependencyId);
                var indicatorId = IndicatorBuilder.BuildIndicatorId(dependencyId);

                // Get indicator
                var dependency = await _mainDbContext.Indicators.FindAsync(userId, indicatorId);

                // Throw NotFound if it does not exist
                if (dependency == null) throw new NotFoundException(string.Format(IndicatorDependencyMessage.IndicatorDependencyNotFound, dependencyId));

                // Add
                dependencies.Add(dependency);
            }

            // Return
            return dependencies;
        }
    }
}
