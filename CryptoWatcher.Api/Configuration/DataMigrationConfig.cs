using CryptoWatcher.Persistence.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatcher.Api.Configuration
{
    public static class DataMigrationConfig
    {
        public static IApplicationBuilder ConfigureDataMigration(this IApplicationBuilder app)
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
