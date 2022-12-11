#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["backend/modules/crypt/Crypt.EventProcessor/Crypt.EventProcessor.csproj", "backend/modules/crypt/Crypt.EventProcessor/"]
RUN dotnet restore "backend/modules/crypt/Crypt.EventProcessor/Crypt.EventProcessor.csproj"
COPY . .
WORKDIR "/src/backend/modules/crypt/Crypt.EventProcessor"
RUN dotnet build "Crypt.EventProcessor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Crypt.EventProcessor.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Crypt.EventProcessor.dll"]