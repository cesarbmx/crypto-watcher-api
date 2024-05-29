using CesarBmx.CryptoWatcher.Api.Configuration;
using CesarBmx.CryptoWatcher.Application.Settings;
using CesarBmx.Shared.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);


var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.

#region Services

// Logging
//services.ConfigureLogging();

// Settings
services.ConfigureSettings(configuration);

// CORS
services.ConfigureCors(configuration);

// Automapper
services.ConfigureAutomapper();

// Swagger
services.ConfigureSwagger();

// DI
services.ConfigureDependencies(configuration);

// Data migration
services.ConfigureDataSeeding();

// Hangfire
services.ConfigureHangfire(configuration);

// Elmah
services.ConfigureElmah();

// Authentication
services.ConfigureAuthentication(configuration);

// Authorization
services.ConfigureAuthorization();

// Distributed caching
services.ConfigureCaching();

// Redis
services.ConfigureRedis(configuration);

// Mvc
services.ConfigureMvc(configuration);

// Health
services.ConfigureHealth(configuration);

// Fluent validation
services.ConfigureFluentValidation();

// Open telemetry
services.ConfigureOpenTelemetry(configuration);

// Masstransit
services.ConfigureMasstransit(configuration);

#endregion

#region App

var app = builder.Build();
var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();

// Grab settings
var appSettings = configuration.GetSection<AppSettings>();

// This for the reverse proxy
app.UsePathBase("/" + appSettings.PathBase);

// CORS
app.ConfigureCors();

// Middleware
app.ConfigureMiddleware();

// Serilog
loggerFactory.ConfigureSerilog(configuration);

// Swagger
app.ConfigureSwagger(configuration);

// Hangfire
app.ConfigureHangfire(configuration);

// Elmah
app.ConfigureElmah(configuration);

// Mvc
app.ConfigureMvc(configuration);

// Health
app.ConfigureHealth();

// Open telemetry
app.ConfigureOpenTelemetry();

// Masstransit
app.ConfigureMasstransit();

app.Run();

#endregion