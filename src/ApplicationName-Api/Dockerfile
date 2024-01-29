FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./ApplicationName.Api/ApplicationName.Api.csproj", "./"]
COPY ["./ApplicationName.Application/ApplicationName.Application.csproj", "./"]
COPY ["./ApplicationName.Common/ApplicationName.Common.csproj", "./"]
COPY ["./ApplicationName.Domain/ApplicationName.Domain.csproj", "./"]
COPY ["./ApplicationName.Infrastructure/ApplicationName.Infrastructure.csproj", "./"]
COPY ["./ApplicationName.Repositories/ApplicationName.Repositories.csproj", "./"]
COPY Directory.Build.props ./
RUN dotnet restore "ApplicationName.Api.csproj"
COPY . .
WORKDIR "/src/ApplicationName.Api"
RUN dotnet build "ApplicationName.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build


FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ApplicationName.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS="https://+:443"
ENV ASPNETCORE_HTTPS_PORT=5001
ENV ASPNETCORE_ENVIRONMENT=Docker
ENV ASPNETCORE_Kestrel__Certificates__Default__Password="Qwerty1234!"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx

ENTRYPOINT [ "dotnet", "ApplicationName.Api.dll" ]