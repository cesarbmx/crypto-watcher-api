using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatcher.Api.Configuration
{
    public static class CorsConfig
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder 
                            //.WithOrigins()
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            //.AllowCredentials()
                            ;
                    });
            });

            return services;
        }
        public static IApplicationBuilder ConfigureCors(this IApplicationBuilder app)
        {
            app.UseCors("AllowAll");

            return app;
        }
    }
}
