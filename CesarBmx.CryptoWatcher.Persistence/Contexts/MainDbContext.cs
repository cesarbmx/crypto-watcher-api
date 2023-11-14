using System.Diagnostics.CodeAnalysis;
using CesarBmx.CryptoWatcher.Domain.Models;
using CesarBmx.CryptoWatcher.Persistence.Mappings;
using CesarBmx.Shared.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CesarBmx.CryptoWatcher.Persistence.Contexts
{
    public class MainDbContext : DbContext
    {
        public DbSet<Line> Lines { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Indicator> Indicators { get; set; }
        public DbSet<IndicatorDependency> IndicatorDependencies { get; set; }
        public DbSet<Watcher> Watchers { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options)
           : base(options)
        {
        }

        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainDbContext).Assembly);

            base.OnModelCreating(modelBuilder);

            // Masstransit outbox
            modelBuilder.UseMasstransitOutbox();
        }
    }
}
