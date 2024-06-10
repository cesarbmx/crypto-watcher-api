var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.CesarBmx_CryptoWatcher_ApiService>("apiservice");

builder.AddProject<Projects.CesarBmx_CryptoWatcher_Web>("webfrontend")
    .WithReference(cache)
    .WithReference(apiService);

builder.Build().Run();
