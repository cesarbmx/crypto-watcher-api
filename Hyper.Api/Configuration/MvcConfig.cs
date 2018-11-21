using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using CryptoWatcher.Api.ActionFilters;
using CryptoWatcher.Api.Controllers;

namespace CryptoWatcher.Api.Configuration
{
    public static class MvcConfig
    {
        public static IServiceCollection ConfigureMvc(this IServiceCollection services)
        {
            services.AddMvc(
                    config =>
                    {
                        config.Filters.Add(typeof(ValidateRequestAttribute));
                        config.Filters.Add(typeof(LowercaseNaturalKeysAttribute));
                    })
                .AddFluentValidation(fv => fv
                    .RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                    .RunDefaultMvcValidationAfterFluentValidationExecutes = false)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; // Ignore nulls
                    options.SerializerSettings.ContractResolver =
                        new DefaultContractResolver()
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        };
                    //new CamelCasePropertyNamesContractResolver(); // CamelCase properties
                    options.SerializerSettings.Converters.Add(
                        new Newtonsoft.Json.Converters.StringEnumConverter()); // Enums as string
                });

            return services;
        }

        public static IApplicationBuilder ConfigureMvc(this IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                // Avoid hosting custom 404 page
                routes.MapRoute(
                    name: "default",
                    template: "{*uri}",
                    defaults: new { controller = typeof(Z_ServiceStatusController).Name.Replace("Controller", ""), action = "ResourceNotFound" });
            });

            return app;
        }
    }
}
