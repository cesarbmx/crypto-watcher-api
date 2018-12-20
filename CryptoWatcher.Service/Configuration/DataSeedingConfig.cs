using CryptoWatcher.Shared.Contexts;
using Microsoft.Extensions.DependencyInjection;
using MainDbContext = CryptoWatcher.Persistence.Contexts.MainDbContext;

namespace CryptoWatcher.Service.Configuration
{
    public static class DataSeedingConfig
    {

        public static void ConfigureDataSeeding(this ServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetService<IServiceScopeFactory>().CreateScope())
            {
                var mainDbContext = serviceScope.ServiceProvider.GetService<IContext>();
                //mainDbContext.Database.Migrate();            
                (mainDbContext as MainDbContext)?.Database.EnsureCreated();
                (mainDbContext as MainDbContext)?.SaveChanges();
            }
        }
    }
}
