# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Auth/QUS.Auth.API/QUS.Auth.API.csproj", "src/Auth/QUS.Auth.API/"]
COPY ["src/Auth/QUS.Auth.Application/QUS.Auth.Application.csproj", "src/Auth/QUS.Auth.Application/"]
COPY ["src/Core/QUS.Core/QUS.Core.csproj", "src/Core/QUS.Core/"]
COPY ["src/Auth/QUS.Auth.Domain/QUS.Auth.Domain.csproj", "src/Auth/QUS.Auth.Domain/"]
COPY ["src/Auth/QUS.Auth.Data/QUS.Auth.Data.csproj", "src/Auth/QUS.Auth.Data/"]
RUN dotnet restore "./src/Auth/QUS.Auth.API/QUS.Auth.API.csproj"
COPY . .
WORKDIR "/src/src/Auth/QUS.Auth.API"
RUN dotnet build "./QUS.Auth.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./QUS.Auth.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
USER root

RUN apt-get update && apt-get install -y \
    libgssapi-krb5-2 \
    && rm -rf /var/lib/apt/lists/*
USER app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QUS.Auth.API.dll"]