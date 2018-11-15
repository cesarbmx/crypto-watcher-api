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
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<ICacheRepository, CacheAuditRepository>();
            services.AddScoped<ILogRepository, LogRepository>();

            // Other
            services.AddScoped<HttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICoinMarketCapClient, CoinMarketCapClient>();
            services.AddScoped(factory => Assembly.GetExecutingAssembly());
            services.AddScoped<IPrincipal>( x => x.GetService<IHttpContextAccessor>().HttpContext.User);

            return services;
        }
    }

    public class DateTimeProvider: IDateTimeProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DateTimeProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DateTime GetDate()
        {
            var date = DateTime.Today.AddDays(1);
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null) return date;

            var header = httpContext.Request.Headers["X-Audit-Date"];
            if(header.Count==0 || !DateTime.TryParse(header, null, System.Globalization.DateTimeStyles.RoundtripKind, out date))
                return date;
            return date.AddDays(1);
        }
    }
}
