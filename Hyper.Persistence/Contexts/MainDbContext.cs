using System.Diagnostics.CodeAnalysis;
using Hyper.Domain.Models;
using Hyper.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Hyper.Persistence.Contexts
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class MainDbContext : DbContext
    {
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Cache> Cache { get; set; }
        public DbSet<Log> Log { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options)
           : base(options)
        {
        }

        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new EntityMap(modelBuilder.Entity<Entity>());
            new CacheMap(modelBuilder.Entity<Cache>());
            new LogMap(modelBuilder.Entity<Log>());
            new CurrencyMap(modelBuilder.Entity<Currency>());          

            base.OnModelCreating(modelBuilder);
        }
    }
}
