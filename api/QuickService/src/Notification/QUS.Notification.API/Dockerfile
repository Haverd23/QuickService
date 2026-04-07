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
COPY ["src/Notification/QUS.Notification.API/QUS.Notification.API.csproj", "src/Notification/QUS.Notification.API/"]
COPY ["src/Notification/QUS.Notification.Data/QUS.Notification.Data.csproj", "src/Notification/QUS.Notification.Data/"]
COPY ["src/Notification/QUS.Notification.Application/QUS.Notification.Application.csproj", "src/Notification/QUS.Notification.Application/"]
COPY ["src/Core/QUS.Core/QUS.Core.csproj", "src/Core/QUS.Core/"]
RUN dotnet restore "./src/Notification/QUS.Notification.API/QUS.Notification.API.csproj"
COPY . .
WORKDIR "/src/src/Notification/QUS.Notification.API"
RUN dotnet build "./QUS.Notification.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./QUS.Notification.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
USER root

RUN apt-get update && apt-get install -y \
    libgssapi-krb5-2 \
    && rm -rf /var/lib/apt/lists/*
USER app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QUS.Notification.API.dll"]