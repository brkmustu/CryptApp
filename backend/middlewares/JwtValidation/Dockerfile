#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["backend/middlewares/JwtValidation/JwtValidation.csproj", "backend/middlewares/JwtValidation/"]
COPY ["backend/modules/auth/Auth.Domain/Auth.Domain.csproj", "backend/modules/auth/Auth.Domain/"]
COPY ["backend/modules/core/Core.Shared/Core.Shared.csproj", "backend/modules/core/Core.Shared/"]
RUN dotnet restore "backend/middlewares/JwtValidation/JwtValidation.csproj"
COPY . .
WORKDIR "/src/backend/middlewares/JwtValidation"
RUN dotnet build "JwtValidation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JwtValidation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JwtValidation.dll"]