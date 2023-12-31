﻿using ApplicationName.Common.Json;

namespace ApplicationName.Api.Extensions;

public static class ApiExtensions
{
    public static void AddApi(this IServiceCollection services)
    {
        services.AddVersioning();
#if HealthCheck
        services.AddHealthChecks();
#endif
        services.AddHttpClient();
        services.AddHttpContextAccessor();
        services.AddHsts();
        services.AddCors();
        services.AddControllers().AddJsonOptions(options => CustomJsonOptions.Configure(options.JsonSerializerOptions));
        services.AddEndpointsApiExplorer();
        services.AddSwagger();
    }
}