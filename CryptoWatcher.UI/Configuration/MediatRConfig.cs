using CryptoWatcher.Application.Requests;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace CryptoWatcher.UI.Configuration
{
    public static class MediatRConfig
    {
        public static IServiceCollection ConfigureMediatR(this IServiceCollection services)
        {
            services.AddMediatR();
            services.AddMediatR(typeof(AddUserRequest).Assembly);

            return services;
        }
    }
}
