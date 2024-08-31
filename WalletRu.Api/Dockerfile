FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WalletRu.Api/WalletRu.Api.csproj", "WalletRu.Api/"]
COPY ["WalletRu.Application/WalletRu.Application.csproj", "WalletRu.Application/"]
COPY ["WalletRu.Domain/WalletRu.Domain.csproj", "WalletRu.Domain/"]
COPY ["WalletRu.DAL/WalletRu.DAL.csproj", "WalletRu.DAL/"]
RUN dotnet restore "WalletRu.Api/WalletRu.Api.csproj"
COPY . .
WORKDIR "/src/WalletRu.Api"
RUN dotnet build "WalletRu.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WalletRu.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WalletRu.Api.dll"]
