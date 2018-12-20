using CryptoWatcher.Shared.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MainDbContext = CryptoWatcher.Persistence.Contexts.MainDbContext;

namespace CryptoWatcher.UI.Configuration
{
    public static class DataSeedingConfig
    {

        public static IApplicationBuilder ConfigureDataSeeding(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var mainDbContext = serviceScope.ServiceProvider.GetService<IContext>();
                //mainDbContext.Database.Migrate();
                (mainDbContext as MainDbContext)?.Database.EnsureCreated();
                (mainDbContext as MainDbContext)?.SaveChanges();
            }

            return app;
        }
    }
}
