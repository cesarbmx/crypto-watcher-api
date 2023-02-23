#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CesarBmx.CryptoWatcher.Api2/CesarBmx.CryptoWatcher.Api2.csproj", "CesarBmx.CryptoWatcher.Api2/"]
RUN dotnet restore "CesarBmx.CryptoWatcher.Api2/CesarBmx.CryptoWatcher.Api2.csproj"
COPY . .
WORKDIR "/src/CesarBmx.CryptoWatcher.Api2"
RUN dotnet build "CesarBmx.CryptoWatcher.Api2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CesarBmx.CryptoWatcher.Api2.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CesarBmx.CryptoWatcher.Api2.dll"]