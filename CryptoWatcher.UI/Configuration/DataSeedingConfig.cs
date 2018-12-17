using CryptoWatcher.Persistence.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatcher.UI.Configuration
{
    public static class DataSeedingConfig
    {

        public static IApplicationBuilder ConfigureDataSeeding(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var mainDbContext = serviceScope.ServiceProvider.GetService<MainDbContext>();
                //mainDbContext.Database.Migrate();
                mainDbContext.Database.EnsureCreated();
                mainDbContext.SaveChanges();
            }

            return app;
        }
    }
}
