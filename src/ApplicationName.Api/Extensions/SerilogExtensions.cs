﻿using Serilog;

namespace ApplicationName.Api.Extensions;

public static class SerilogExtensions
{
    public static void UseSerilog(this IHostBuilder hostBuilder) =>
        hostBuilder.UseSerilog((hostingContext, _, loggerConfiguration) => loggerConfiguration
            .ReadFrom.Configuration(hostingContext.Configuration)
            .Enrich.FromLogContext()
            .Enrich.WithClientIp()
        );
}