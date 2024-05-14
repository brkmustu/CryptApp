#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["backend/services/Crypt.Service/Crypt.Service.csproj", "backend/services/Crypt.Service/"]
COPY ["backend/modules/core/Core.Shared/Core.Shared.csproj", "backend/modules/core/Core.Shared/"]
COPY ["backend/modules/core/Messaging/Messaging.csproj", "backend/modules/core/Messaging/"]
COPY ["backend/modules/crypt/Crypt.Domain.Shared/Crypt.Domain.Shared.csproj", "backend/modules/crypt/Crypt.Domain.Shared/"]
RUN dotnet restore "backend/services/Crypt.Service/Crypt.Service.csproj"
COPY . .
WORKDIR "/src/backend/services/Crypt.Service"
RUN dotnet build "Crypt.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Crypt.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Crypt.Service.dll"]