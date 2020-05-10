using CesarBmx.Shared.Api.ActionFilters;
using CesarBmx.Shared.Api.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CryptoWatcher.Application.Validators;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace CryptoWatcher.Api.Configuration
{
    public static class MvcConfig
    {
        public static IServiceCollection ConfigureMvc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(
                    config =>
                    {
                        // Authentication
                        var policy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();
                        config.Filters.Add(new AuthorizeFilter(policy));

                        // Filters
                        config.Filters.Add(typeof(ValidateRequestAttribute));
                        config.Filters.Add(typeof(IdentityFilter));
                    })
                .ConfigureFluentValidation(typeof(AddUserValidator).Assembly)
                .ConfigureSharedSerialization();

            services.AddRazorPages();

            services.AddRouting(options => options.LowercaseUrls = true);

            // Allow synchronous IO (elmah css was not loading)
            services.Configure<IISServerOptions>(options => { options.AllowSynchronousIO = true; });
            services.Configure<KestrelServerOptions>(options => { options.AllowSynchronousIO = true; });

            return services;
        }

        public static IApplicationBuilder ConfigureMvc(this IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
            app.UseStaticFiles();

            return app;
        }
    }
}
