#if efsql
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
#endif
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationName.Infrastructure.Persistence;

public static class PersistenceExtensions
{
#if (efsql)
    public static void AddPersistence(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(builder => builder.ConfigureDbContext(connectionString));
    }

    private static void ConfigureDbContext(this DbContextOptionsBuilder builder, string connectionString) =>
        builder
            .UseSqlServer(connectionString)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .ConfigureWarnings(x => x.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
#endif
#if (!efsql)
    public static void AddPersistence(this IServiceCollection services)
    {
    }
#endif
}