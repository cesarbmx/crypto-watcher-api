using CryptoWatcher.Application.Services;
using CryptoWatcher.Persistence.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatcher.Api.Configuration
{
    public static class DataSeedingConfig
    {

        public static IApplicationBuilder ConfigureDataSeeding(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var mainDbContext = serviceScope.ServiceProvider.GetService<MainDbContext>();
                var seedService = serviceScope.ServiceProvider.GetService<SeedService>();
                //mainDbContext.Database.Migrate();
                if (mainDbContext.Database.EnsureCreated()) seedService.Seed().Wait();
                mainDbContext.SaveChanges();
            }

            return app;
        }
    }
}
