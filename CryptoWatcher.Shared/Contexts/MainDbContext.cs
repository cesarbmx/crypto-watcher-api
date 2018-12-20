using System.Diagnostics.CodeAnalysis;
using CryptoWatcher.Shared.Mappings;
using CryptoWatcher.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Shared.Contexts
{
    public class MainDbContext : DbContext, IContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options)
           : base(options)
        {
        }
        protected MainDbContext(DbContextOptions options) //https://github.com/aspnet/EntityFrameworkCore/issues/7533#issuecomment-353669263
            : base(options)
        {
        }

        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new LogMap(modelBuilder.Entity<Log>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
