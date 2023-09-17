#if efsql
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics
using Microsoft.Extensions.DependencyInjection;;
#elif dappersql
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
#else
using Microsoft.Extensions.DependencyInjection;
#endif

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
#elif (dappersql)
    public static void AddPersistence(this IServiceCollection services, string connectionString)
    {
        services.AddTransient<IDbConnection>(_ => new SqlConnection(connectionString));
    }
#else
    public static void AddPersistence(this IServiceCollection services)
    {
    }
#endif
}