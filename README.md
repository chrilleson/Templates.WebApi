# Templates.WebApi

This project provides a template solution for web api's. It uses a CQRS architechure. All your logic should be contained within the `Application` project. The `Api` should just send a new command or query via `MediatR` and then the request and handler is located inside of the `Application`.

## Template Parameters

When you create a new solution using the template via Visual Studio or the .NET CLI you will be asked to specify the following parameters.

Each parameter has a default value, so you can ran with the defaults if you like.

| Parameter Name        | Description                                                                      | Command                   | Default value           |
|-----------------------|----------------------------------------------------------------------------------|---------------------------|-------------------------|
| **Docker**            | Adds an optimised Dockerfile to add the ability to build a Docker image.         | `-docker`, `-d`           | _true_                  |
| **ReadMe**            | Adds a README markdown file describing the project.                              | `-readme`, `-r`           | _true_                  |
| **HealthCheck**       | Enables or disables the use of healthchecks.                                     | `-healthcheck`, `-health` | true                    |
| **HealthCheckPath**   | HealthCheck path. Only necessary if HealthCheck is enabled.                      | `-healthcheck-path`, `-hp`| /health                 |

---
