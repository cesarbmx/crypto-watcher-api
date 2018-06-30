using System.Diagnostics.CodeAnalysis;
using Hyper.Domain.Models;
using Hyper.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Hyper.Infrastructure.Contexts
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class MainDbContext : DbContext
    {
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Cache> Cache { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options)
           : base(options)
        {
        }

        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new CurrencyMap(modelBuilder.Entity<Currency>());
            new CacheMap(modelBuilder.Entity<Cache>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
