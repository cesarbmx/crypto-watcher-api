using System.Diagnostics.CodeAnalysis;
using CesarBmx.Shared.Domain.Entities;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Contexts
{
    public class MainDbContext : DbContext
    {
        public DbSet<Line> Lines { get; set; }
        public DbSet<AuditLog> AuditLog { get; set; }
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
            modelBuilder.Entity<Line>().Map();
            modelBuilder.Entity<AuditLog>().Map();
            modelBuilder.Entity<Currency>().Map();
            modelBuilder.Entity<Watcher>().Map();
            modelBuilder.Entity<User>().Map();
            modelBuilder.Entity<Notification>().Map();
            modelBuilder.Entity<Order>().Map();
            modelBuilder.Entity<Indicator>().Map();
            modelBuilder.Entity<IndicatorDependency>().Map();

            base.OnModelCreating(modelBuilder);
        }
    }
}
