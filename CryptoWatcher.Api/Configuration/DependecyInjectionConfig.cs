using CoinMarketCap;
using CoinMarketCap.Core;
using CryptoWatcher.Application.Services;
using CryptoWatcher.BackgroundJobs;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatcher.Api.Configuration
{
    public static class DependecyInjectionConfig
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // UseMemoryStorage
            if (bool.Parse(configuration["AppSettings:UseMemoryStorage"]))
            {
                //Contexts (UOW)
                services.AddDbContext<MainDbContext>(options => options
                    .UseInMemoryDatabase("CryptoWatcher")
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            }
            else
            {
                //Contexts (UOW)
                services.AddDbContext<MainDbContext>(options => options
                    .UseSqlServer(configuration.GetConnectionString("CryptoWatcher"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            }

            // Services
            services.AddScoped<LogService, LogService>();
            services.AddScoped<CurrencyService, CurrencyService>();
            services.AddScoped<WatcherService, WatcherService>();
            services.AddScoped<UserService, UserService>();
            services.AddScoped<NotificationService, NotificationService>();
            services.AddScoped<OrderService, OrderService>();
            services.AddScoped<IndicatorService, IndicatorService>();
            services.AddScoped<LineService, LineService>();
            services.AddScoped<StatusService, StatusService>();
            services.AddScoped<LineChartService, LineChartService>();
            services.AddScoped<ScriptVariableService, ScriptVariableService>();

            //// Repositories
            services.AddScoped<Repository<Log>, Repository<Log>>();
            services.AddScoped<Repository<Currency>, Repository<Currency>>();
            services.AddScoped<Repository<Watcher>, Repository<Watcher>>();
            services.AddScoped<Repository<User>, Repository<User>>();
            services.AddScoped<Repository<Notification>, Repository<Notification>>();
            services.AddScoped<Repository<Order>, Repository<Order>>();
            services.AddScoped<Repository<Indicator>, Repository<Indicator>>();
            services.AddScoped<Repository<IndicatorDependency>, Repository<IndicatorDependency>>();
            services.AddScoped<Repository<Line>, Repository<Line>>();

            //// Logger repositories
            services.AddScoped<IRepository<Log>, Repository<Log>>();
            services.AddScoped<IRepository<Currency>, LoggerRepository<Currency>>();
            services.AddScoped<IRepository<Watcher>, LoggerRepository<Watcher>>();
            services.AddScoped<IRepository<User>, LoggerRepository<User>>();
            services.AddScoped<IRepository<Notification>, LoggerRepository<Notification>>();
            services.AddScoped<IRepository<Order>, LoggerRepository<Order>>();
            services.AddScoped<IRepository<Indicator>, LoggerRepository<Indicator>>();
            services.AddScoped<IRepository<Line>, LoggerRepository<Line>>();
            services.AddScoped<IRepository<IndicatorDependency>, LoggerRepository<IndicatorDependency>>();
            services.AddScoped<IRepository<Line>, LoggerRepository<Line>>();

            // Audit repositories
            //services.AddScoped<IRepository<Log>, Repository<Log>>();
            //services.AddScoped<IRepository<Currency>, AuditRepository<Currency>>();
            //services.AddScoped<IRepository<Watcher>, AuditRepository<Watcher>>();
            //services.AddScoped<IRepository<User>, AuditRepository<User>>();
            //services.AddScoped<IRepository<Notification>, AuditRepository<Notification>>();
            //services.AddScoped<IRepository<Order>, AuditRepository<Order>>();
            //services.AddScoped<IRepository<Indicator>, AuditRepository<Indicator>>();
            //services.AddScoped<IRepository<Line>, AuditRepository<Line>>();
            //services.AddScoped<IRepository<IndicatorDependency>, AuditRepository<IndicatorDependency>>();
            //services.AddScoped<IRepository<Line>, LineAuditRepository>();

            // Jobs
            services.AddScoped<MainJob, MainJob>();
            services.AddScoped<UpdateCurrenciesJob, UpdateCurrenciesJob>();
            services.AddScoped<UpdateLinesJob, UpdateLinesJob>();
            services.AddScoped<UpdateDefaultWatchersJob, UpdateDefaultWatchersJob>();
            services.AddScoped<UpdateWatchersJob, UpdateWatchersJob>();
            services.AddScoped<UpdateOrdersJob, UpdateOrdersJob>();
            services.AddScoped<SendWhatsappNotificationsJob, SendWhatsappNotificationsJob>();
            services.AddScoped<SendTelgramNotifications, SendTelgramNotifications>();
            services.AddScoped<RemoveLinesJob, RemoveLinesJob>();
            services.AddScoped<UpdateIndicatorDependenciesJob, UpdateIndicatorDependenciesJob>();

            // Other
            //services.AddScoped<DateTimeProvider, DateTimeProvider>();
            //services.AddScoped<HttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICoinMarketCapClient, CoinMarketCapClient>();

            return services;
        }
    }
}
