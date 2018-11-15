using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace Hyper.Api.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                // Define the OAuth2.0 scheme that's in use (i.e. Implicit Flow)
                //c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                //{
                //    Type = "oauth2",
                //    Flow = "implicit",
                //    AuthorizationUrl = $"{configuration["IdentityServer:Sts"]}connect/authorize",
                //    Scopes = new Dictionary<string, string>
                //    {
                //        {configuration["IdentityServer:RequiredScopes"], "Hyper"}
                //    }
                //});

                // Define the API Key scheme that's in use
                c.AddSecurityDefinition("ApiKey", new ApiKeyScheme { In = "header", Description = "Please enter your API Key", Name = "X-API-Key", Type = "apiKey" });
                c.AddSecurityDefinition("AuditDate", new ApiKeyScheme { In = "header", Description = "Please enter your date (e.g 2018-11-15)", Name = "X-Audit-Date", Type = "apiKey" });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                    { "ApiKey", Enumerable.Empty<string>() },
                    { "AuditDate", Enumerable.Empty<string>() }
                });

                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Hyper API",
                        Version = "v1"
                    }); // Register the Swagger generator, defining one or more Swagger documents
                c.ExampleFilters(); // [SwaggerRequestExample] & [SwaggerResponseExample]
                //c.OperationFilter<DescriptionOperationFilter>(); // [Description] on Response properties
                //c.OperationFilter<AuthorizationInputOperationFilter>(); // Adds an Authorization input box to every endpoint
                c.OperationFilter<AddFileParamTypesOperationFilter>(); // Adds an Upload button to endpoints which have [AddSwaggerFileUploadButton]
                //c.OperationFilter<AddHeaderOperationFilter>("CorrelationId", "Correlation Id for the request"); // adds any string you like to the request headers - in this case, a correlation id
                c.OperationFilter<AddResponseHeadersFilter>(); // [SwaggerResponseHeader]
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>(); // Adds "(Auth)" to the summary so that you can see which endpoints have Authorization                
                c.EnableAnnotations(); // Enables the groupings (TAGs)

                // XML documentation file
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                var commentsFile = Path.Combine(baseDirectory, commentsFileName);
                c.IncludeXmlComments(commentsFile);
            });

            // Add swagger examples
            services.AddSwaggerExamples();

            return services;
        }

        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "Hyper");
                c.RoutePrefix = string.Empty; // Serve the Swagger UI at the app's root
                c.OAuthClientId(configuration["IdentityServer:ClientId"]);
                c.OAuth2RedirectUrl(configuration["Swagger:RootUrlFrom"] + "swagger/ui/o2c-html");
            });

            return app;
        }
    }
}
