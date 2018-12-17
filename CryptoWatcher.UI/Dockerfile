FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["CryptoWatcher.UI/CryptoWatcher.UI.csproj", "CryptoWatcher.UI/"]
RUN dotnet restore "CryptoWatcher.UI/CryptoWatcher.UI.csproj"
COPY . .
WORKDIR "/src/CryptoWatcher.UI"
RUN dotnet build "CryptoWatcher.UI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CryptoWatcher.UI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CryptoWatcher.UI.dll"]