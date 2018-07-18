using System.Reflection;
using System.Security.Principal;
using CoinMarketCap;
using CoinMarketCap.Core;
using Hyper.Domain.Repositories;
using Hyper.Domain.Services;
using Hyper.Infrastructure.Contexts;
using Hyper.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
            //services.AddDbContext<MainDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Hyper")));
            services.AddDbContext<MainDbContext>(options => options.UseInMemoryDatabase("Hyper"));

            //Services
            services.AddScoped<CacheService, CacheService>();
            services.AddScoped<CurrencyService, CurrencyService>();
            services.AddScoped<StatusService, StatusService>();
            services.AddScoped<ErrorMessagesService, ErrorMessagesService>();

            //Repositories
            services.AddScoped<ICacheRepository, CacheRepository>();

            // Other
            services.AddScoped<ICoinMarketCapClient, CoinMarketCapClient>();
            services.AddScoped(factory => Assembly.GetExecutingAssembly());
            services.AddScoped<IPrincipal>( x => x.GetService<IHttpContextAccessor>().HttpContext.User);

            return services;
        }
    }
}
