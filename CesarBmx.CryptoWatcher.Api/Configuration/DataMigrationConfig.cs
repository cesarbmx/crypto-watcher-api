using System;
using CesarBmx.CryptoWatcher.Persistence.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CesarBmx.CryptoWatcher.Api.Configuration
{
    public static class DataMigrationConfig
    {
        public static IApplicationBuilder ConfigureDataMigration(this IApplicationBuilder app)
        {

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                // Get MainDbContext
                var mainDbContext = serviceScope.ServiceProvider.GetService<MainDbContext>();
                
                // Make sure it gets resolved
                if (mainDbContext == null) throw new ArgumentException("MainDbContext is expected");

                //mainDbContext.Database.Migrate();
                mainDbContext.Database.EnsureCreated();
                mainDbContext.SaveChanges();
            }

            return app;
        }
    }
}
