using System.Diagnostics.CodeAnalysis;
using CesarBmx.Shared.Domain.Models;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Contexts
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options)
           : base(options)
        {
        }

        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new LineMap(modelBuilder.Entity<Line>());
            new LogMap(modelBuilder.Entity<Log>());
            new CurrencyMap(modelBuilder.Entity<Currency>());
            new WatcherMap(modelBuilder.Entity<Watcher>());
            new UserMap(modelBuilder.Entity<User>());
            new NotificationMap(modelBuilder.Entity<Notification>());
            new OrderMap(modelBuilder.Entity<Order>());
            new IndicatorMap(modelBuilder.Entity<Indicator>());
            new IndicatorDependencyMap(modelBuilder.Entity<IndicatorDependency>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
