#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["backend/services/Auth.Service/Auth.Service.csproj", "backend/services/Auth.Service/"]
COPY ["backend/modules/auth/Auth.Application/Auth.Application.csproj", "backend/modules/auth/Auth.Application/"]
COPY ["backend/modules/core/Caching/Caching.csproj", "backend/modules/core/Caching/"]
COPY ["backend/modules/core/Core.Shared/Core.Shared.csproj", "backend/modules/core/Core.Shared/"]
COPY ["backend/modules/auth/Auth.Domain/Auth.Domain.csproj", "backend/modules/auth/Auth.Domain/"]
RUN dotnet restore "backend/services/Auth.Service/Auth.Service.csproj"
COPY . .
WORKDIR "/src/backend/services/Auth.Service"
RUN dotnet build "Auth.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Auth.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auth.Service.dll"]