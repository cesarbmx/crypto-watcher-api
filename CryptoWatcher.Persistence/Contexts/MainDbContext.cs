using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Contexts
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class MainDbContext : DbContext
    {
        public DbSet<Line> Lines { get; set; }
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
            new LineMap(modelBuilder.Entity<Line>());
            new CurrencyMap(modelBuilder.Entity<Currency>());
            new WatcherMap(modelBuilder.Entity<Watcher>());
            new UserMap(modelBuilder.Entity<User>());
            new NotificationMap(modelBuilder.Entity<Notification>());
            new OrderMap(modelBuilder.Entity<Order>());
            new IndicatorMap(modelBuilder.Entity<Indicator>());
            new IndicatorDependencyMap(modelBuilder.Entity<IndicatorDependency>());

            base.OnModelCreating(modelBuilder);
        }

        public void UpdateCollection<TEntity>(List<TEntity> currentEntities, List<TEntity> newEntities) where TEntity : class, IEntity
        {
            AddRange(EntityBuilder.BuildEntitiesToAdd(currentEntities, newEntities));
            UpdateRange(EntityBuilder.BuildEntitiesToUpdate(currentEntities, newEntities));
            RemoveRange(EntityBuilder.BuildEntitiesToRemove(currentEntities, newEntities));
        }
    }
}
