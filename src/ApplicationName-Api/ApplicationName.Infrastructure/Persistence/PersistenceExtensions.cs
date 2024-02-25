#if ef
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
#elif (dapper && sqlServer)
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
#elif (dapper && postgres)
using Npgsql;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
#else
using Microsoft.Extensions.DependencyInjection;
#endif

namespace ApplicationName.Infrastructure.Persistence;

public static class PersistenceExtensions
{
#if (ef && sqlServer)
    public static void AddPersistence(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(builder => builder.ConfigureDbContext(connectionString));
    }

    private static void ConfigureDbContext(this DbContextOptionsBuilder builder, string connectionString) =>
         builder
             .UseSqlServer(connectionString)
             .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
             .ConfigureWarnings(x => x.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
#elif (ef && postgres)
     public static void AddPersistence(this IServiceCollection services, string connectionString)
     {
         services.AddDbContext<AppDbContext>(builder => builder.ConfigureDbContext(connectionString));
     }

    private static void ConfigureDbContext(this DbContextOptionsBuilder builder, string connectionString) =>
        builder
            .UseNpgsql(connectionString)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .ConfigureWarnings(x => x.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
#elif (ef)
     public static void AddPersistence(this IServiceCollection services, string connectionString)
     {
         services.AddDbContext<AppDbContext>(builder => builder.ConfigureDbContext(connectionString));
     }

    private static void ConfigureDbContext(this DbContextOptionsBuilder builder) =>
        builder
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .ConfigureWarnings(x => x.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
#elif (dapper && sqlServer)
    public static void AddPersistence(this IServiceCollection services, string connectionString)
    {
        services.AddTransient<IDbConnection>(_ => new SqlConnection(connectionString));
    }
#elif (dapper && postgres)
    public static void AddPersistence(this IServiceCollection services, string connectionString)
    {
        services.AddTransient<IDbConnection>(_ => new NpgsqlConnection(connectionString));
    }
#elif (dapper)
    public static void AddPersistence(this IServiceCollection services, string connectionString)
    {
        services.AddTransient<IDbConnection>(_ => /*Your DB Connection here*/);
    }
#else
    public static void AddPersistence(this IServiceCollection services)
    {
    }
#endif
}