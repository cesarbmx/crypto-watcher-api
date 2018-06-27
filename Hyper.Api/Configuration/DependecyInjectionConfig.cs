using System.Reflection;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hyper.Api.Configuration
{
    public static class DependecyInjectionConfig
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
        {

            //services.AddScoped<IPinnacleTokenService<HyperPermission>, PinnacleTokenService<HyperPermission>>();

            //Contexts (UOW)

            //Services
         
            //Repositories

            // Other
            services.AddScoped(factory => Assembly.GetExecutingAssembly());
            services.AddScoped<IPrincipal>( x => x.GetService<IHttpContextAccessor>().HttpContext.User);

            return services;
        }
    }
}
