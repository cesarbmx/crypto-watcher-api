using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CryptoWatcher.Persistence.Contexts;

namespace CryptoWatcher.Api.Configuration
{
    public static class DataMigrationConfig
    {
        public static IApplicationBuilder ConfigureDataMigration(this IApplicationBuilder app, IConfiguration configuration)
        {

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var mainDbContext = serviceScope.ServiceProvider.GetService<MainDbContext>();
                //mainDbContext.Database.Migrate();
                //mainDbContext.Database.EnsureCreated();
                mainDbContext.SaveChanges();
            }

            return app;
        }
    }
}
