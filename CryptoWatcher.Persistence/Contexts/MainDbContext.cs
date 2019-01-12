using System.Diagnostics.CodeAnalysis;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Contexts
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class MainDbContext : DbContext
    {
        public DbSet<DataPoint> Lines { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Watcher> Watchers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Indicator> Indicators { get; set; }
        public DbSet<IndicatorDependency> IndicatorDependencies { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options)
           : base(options)
        {
        }

        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new LineMap(modelBuilder.Entity<DataPoint>());
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
