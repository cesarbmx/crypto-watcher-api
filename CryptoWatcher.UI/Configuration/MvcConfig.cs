using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CryptoWatcher.UI.Configuration
{
    public static class MvcConfig
    {
        public static IServiceCollection ConfigureMvc(this IServiceCollection services)
        {
            services.AddMvc()
                .AddFluentValidation(fv => fv
                    .RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly())
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
                routes.MapRoute(
                    name: "default",
                    template: "{controller=LineCharts}/{action=Index}/{id?}");
            });

            return app;
        }
    }
}
