using CryptoWatcher.Api.ActionFilters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using CryptoWatcher.Api.Controllers;
using CryptoWatcher.Application.Users.Validators;

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
                    })
                .AddFluentValidation(fv => fv
                    .RegisterValidatorsFromAssembly(typeof(AddUserValidator).Assembly)
                    .RunDefaultMvcValidationAfterFluentValidationExecutes = false)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; // Ignore nulls
                    options.SerializerSettings.ContractResolver =
                    //new DefaultContractResolver()
                    //{
                    //    NamingStrategy = new  SnakeCaseNamingStrategy()
                    //};
                    new CamelCasePropertyNamesContractResolver(); // CamelCase properties
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
                    defaults: new { controller = typeof(Z_SystemController).Name.Replace("Controller", ""), action = "ResourceNotFound" });
            });

            return app;
        }
    }
}
