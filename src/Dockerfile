FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

# Copy csproj and restore
COPY *.sln .
COPY */*.csproj ./
COPY Directory.Build.props ./

RUN for file in $(ls *.csproj); do mkdir -p ${file%.*} && mv $file ${file%.*}; done
RUN dotnet restore

# Copy before publish
COPY . ./
RUN dotnet publish ApplicationName.Api -o /app

# Switch from sdk container to runtime and copy files
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./


ENTRYPOINT [ "dotnet", "ApplicationName.Api.dll" ]