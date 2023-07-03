using Microsoft.AspNetCore.Mvc;

namespace ApplicationName.Api.Extensions;

public static class ApiVersioningExtensions
{
    public static void AddVersioning(this IServiceCollection services) =>
        services.AddApiVersioning(opt =>
        {
            opt.ReportApiVersions = true;
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.AssumeDefaultVersionWhenUnspecified = true;
        });
}