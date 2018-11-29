using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace CryptoWatcher.Api.Configuration
{
    public static class MediatRConfig
    {
        public static IServiceCollection ConfigureMediatR(this IServiceCollection services)
        {
            services.AddMediatR();

            return services;
        }
    }
}
