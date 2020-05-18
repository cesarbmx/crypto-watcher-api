using CryptoWatcher.Api.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace CryptoWatcher.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Settings
            services.ConfigureSettings(Configuration);

            // CORS
            services.ConfigureCors();

            // Automapper
            services.ConfigureAutomapper();

            // Swagger
            services.ConfigureSwagger();

            // DI
            services.ConfigureDependencies(Configuration);

            // Hangfire
            services.ConfigureHangfire(Configuration);

            // Elmah
            services.ConfigureElmah();

            // Authentication
            services.ConfigureAuthentication(Configuration);

            // Authorization
            services.ConfigureAuthorization();

            // Distributed caching
            services.ConfigureCaching();

            // Mvc
            services.ConfigureMvc();

            // Health
            services.ConfigureHealth(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env, ILoggerFactory loggerFactory)
        {
            // This for the reverse proxy
            app.UsePathBase("/crypto-watcher");

            // CORS
            app.ConfigureCors();

            // Error handling
            app.ConfigureErrorHandling();

            // Log4Net
            loggerFactory.ConfigureLog4Net(env, Configuration);

            // Swagger
            app.ConfigureSwagger(Configuration);

            // Hangfire
            app.ConfigureHangfire(Configuration, env);

            // Elmah
            app.ConfigureElmah(Configuration);

            // Data migration
            app.ConfigureDataMigration();

            // Mvc
            app.ConfigureMvc();

            // Health
            app.ConfigureHealth();
        }
    }
}
