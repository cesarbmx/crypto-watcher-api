using System.Diagnostics.CodeAnalysis;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Contexts
{
    public class MainDbContext : Shared.Contexts.MainDbContext
    {
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

            base.OnModelCreating(modelBuilder);
        }
    }
}
