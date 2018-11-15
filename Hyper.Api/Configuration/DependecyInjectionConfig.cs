using System;
using System.Reflection;
using System.Security.Principal;
using CoinMarketCap;
using CoinMarketCap.Core;
using Hyper.Domain.Repositories;
using Hyper.Domain.Services;
using Hyper.Persistence.AuditRepositories;
using Hyper.Persistence.Contexts;
using Hyper.Persistence.Repositories;
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

            // Services
            services.AddScoped<CacheService, CacheService>();
            services.AddScoped<CurrencyService, CurrencyService>();
            services.AddScoped<StatusService, StatusService>();
            services.AddScoped<ErrorMessagesService, ErrorMessagesService>();
            services.AddScoped<LogService, LogService>();

            // Repositories
            services.AddScoped<ICacheRepository, CacheRepository>();
            //services.AddScoped<ICacheRepository>(x=> new CacheAuditRepository(x.GetService<ILogRepository>(), DateTime.Today.AddDays(1)));
            services.AddScoped<ILogRepository, LogRepository>();

            // Other
            services.AddScoped<ICoinMarketCapClient, CoinMarketCapClient>();
            services.AddScoped(factory => Assembly.GetExecutingAssembly());
            services.AddScoped<IPrincipal>( x => x.GetService<IHttpContextAccessor>().HttpContext.User);

            return services;
        }
    }
}
