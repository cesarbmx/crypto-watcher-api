using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Hyper.Api.Configuration;

namespace Hyper.Api
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
            // CORS
            services.ConfigureCors();

            // Automapper
            services.ConfigureAutomapper();

            // Hangfire
            services.ConfigureHangfire(Configuration);

            // Swagger
            services.ConfigureSwagger(Configuration);

           // DI
            services.ConfigureDependencies(Configuration);

            // Mvc
            services.ConfigureMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory)
        {
            // Middlewares
            app.ConfigureMiddlewares();

            // Log4Net
            loggerfactory.ConfigureLog4Net(env);

            // Swagger
            app.ConfigureSwagger(Configuration);

            // Hangfire
            app.ConfigureHangfire(Configuration);

            // Mvc
            app.ConfigureMvc();
        }
    }
}
