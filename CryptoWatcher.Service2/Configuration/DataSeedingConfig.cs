using CryptoWatcher.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatcher.Service.Configuration
{
    public static class DataSeedingConfig
    {

        public static void ConfigureDataSeeding(this ServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetService<IServiceScopeFactory>().CreateScope())
            {
                var mainDbContext = serviceScope.ServiceProvider.GetService<MainDbContext>();
                //mainDbContext.Database.Migrate();
                mainDbContext.Database.EnsureCreated();
                mainDbContext.SaveChanges();
            }
        }
    }
}
