# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Service/QUS.Service.API/QUS.Service.API/QUS.Service.API.csproj", "src/Service/QUS.Service.API/QUS.Service.API/"]
COPY ["src/Service/QUS.Service.Application/QUS.Service.Application.csproj", "src/Service/QUS.Service.Application/"]
COPY ["src/User/QUS.User.Domain/QUS.Users.Domain.csproj", "src/User/QUS.User.Domain/"]
COPY ["src/Core/QUS.Core/QUS.Core.csproj", "src/Core/QUS.Core/"]
COPY ["src/Service/QUS.Services.Domain/QUS.Services.Domain.csproj", "src/Service/QUS.Services.Domain/"]
COPY ["src/Service/QUS.Services.Data/QUS.Services.Data.csproj", "src/Service/QUS.Services.Data/"]
RUN dotnet restore "./src/Service/QUS.Service.API/QUS.Service.API/QUS.Service.API.csproj"
COPY . .
WORKDIR "/src/src/Service/QUS.Service.API/QUS.Service.API"
RUN dotnet build "./QUS.Service.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./QUS.Service.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
USER root

RUN apt-get update && apt-get install -y \
    libgssapi-krb5-2 \
    && rm -rf /var/lib/apt/lists/*
USER app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QUS.Service.API.dll"]