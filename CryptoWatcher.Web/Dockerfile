FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["CryptoWatcher.Web/CryptoWatcher.Web.csproj", "CryptoWatcher.Web/"]
RUN dotnet restore "CryptoWatcher.Web/CryptoWatcher.Web.csproj"
COPY . .
WORKDIR "/src/CryptoWatcher.Web"
RUN dotnet build "CryptoWatcher.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CryptoWatcher.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CryptoWatcher.Web.dll"]