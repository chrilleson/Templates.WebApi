# Templates.WebApi

![Workflow Status](https://github.com/chrilleson/Templates.WebApi/actions/workflows/release.yaml/badge.svg) [![Templates.WebApi](https://img.shields.io/nuget/v/Templates.WebApi.svg)](https://www.nuget.org/packages/Templates.WebApi/)

## Description

This project provides a template solution for web api's. It uses a CQRS architechure. All your logic should be contained within the `Application` project. The `Api` should just send a new command or query via `MediatR` and then the request and handler is located inside of the `Application`.

## Template Parameters

When you create a new solution using the template via Visual Studio or the .NET CLI you will be asked to specify the following parameters.

| Parameter Name        | Description                                                                      | Command                      | Default value           |
|-----------------------|----------------------------------------------------------------------------------|------------------------------|-------------------------|
| **Docker**            | Adds an optimised Dockerfile to add the ability to build a Docker image.         | `-docker`                    | _true_                  |
| **ReadMe**            | Adds a README markdown file describing the project.                              | `-readme`, `-r`              | _true_                  |
| **HealthCheck**       | Enables or disables the use of healthchecks.                                     | `-healthcheck`, `-health`    | _true_                  |
| **HealthCheckPath**   | HealthCheck path. Only necessary if HealthCheck is enabled.                      | `-healthcheck-path`, `-hp`   | /health                 |
| **EF Core**           | Enables or disables the use of EF-Core.                                          | `-efl`, `--entity-framework` | _false_                 |
| **Dapper**            | Enables or disables the use of Dapper.                                           | `-dapper`                    | _false_                 |
| **SqlServer**         | Enables or disables the use of a SQL server.                                     | `-sql`, `--sqlServer`        | _false_                 |
| **PostgreSQL**        | Enables or disables the use of a PostgreSQL server.                              | `-pg`, `--postgres`          | _false_                 |
| **Weather Api**       | Adds an endpoint and handler to get weather data.                                | `-weather-api`               | _true_                  |
| **Seq**               | Adds Seq as a logging provider.                                                  | `-seq`                       | _true_                  |

### Docker

When `Docker` parameter is `true`, a docker image, dockerignore, and docker-compose file will be included in the project. To run the api in docker, simply go to the root dir and run `docker compose up --build -d`. If you choose to set docker to `false` theese files will not be included. If you choose to also add a database the compose file will setup that service for you and also add `adminer` as a service. Read more about adminer [here](https://www.adminer.org/)

### ReadMe

Adds a simple `readme.md` file.

### Health Check

Adds the use of health checks.

### Health Check Path

Option to add a custom health check path. The default value is `/health`.

### EF Core

Adds packages and refreneces to easily setup Entity Framework Core. This is set to latest version. Comes with a `AppDbContext`.

### Dapper

Adds packages and refreneces to easily setup Dapper.

### SQL Server

Adds a `db` service as a SqlServer to your docker compose file. This uses the official MSSQL Server 2022 latest image. If you want to set this to a fixed version, just change from `latest` in the docker compose file. In the compose fiel you will also find the username and password for you local environment. CAUTION, do not use these in any other environment than your local. This also mount a data dir to the container so that your data will be persisted if you remove the container.

### PostgreSQL

Adds a `db` service as a PostgreSQL to your docker compose file. This uses the official Postgres v.16 image. If you want to set this to a fixed version, just change from `16` in the docker compose file. In the compose fiel you will also find the username and password for you local environment. CAUTION, do not use these in any other environment than your local. This also mount a data dir to the container so that your data will be persisted if you remove the container.

### Weather Api

Adds an default controller and endpoint that handles incoming requests and returns weather data. This is the default weather api implemented in this project.

### Seq

Adds Seq service as a logging provider. Works out of the box, but if you want to add custom application names etc, create a api key in the GUI. Seq has its own volume at `data/seq` where data to the service can be persited.
