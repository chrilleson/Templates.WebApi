namespace ApplicationName.Api.Extensions;

public static class HstsExtensions
{
    public static void AddHsts(this IServiceCollection services) =>
        services.AddHsts(options =>
        {
            options.Preload = true;
            options.IncludeSubDomains = true;
            options.MaxAge = TimeSpan.FromDays(365);
        });
}