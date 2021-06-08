#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 8123

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ENV ASPNETCORE_ENVIRONMENT="Production"
ENV ENVIRONMENT="Production"
WORKDIR /src
COPY ["twatter-API-gateway.csproj", ""]
RUN dotnet restore "./twatter-API-gateway.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "twatter-API-gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "twatter-API-gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "twatter-API-gateway.dll"]