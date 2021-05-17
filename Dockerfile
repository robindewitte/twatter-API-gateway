#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 8123

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["twatter-API-gateway.csproj", "twatter-API-gateway/"]
RUN dotnet restore "twatter-API-gateway.csproj"
COPY . .
WORKDIR "/src/twatter-API-gateway"
RUN dotnet build "twatter-API-gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "twatter-API-gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "twatter-API-gateway.dll"]