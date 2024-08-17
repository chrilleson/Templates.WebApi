#if (ef)
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
#elif (dapper)
    #if (sqlServer && postgres)
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
    #elif (sqlServer)
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
    #elif (postgres)
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
    #endif
using System.Data;
#else
using Microsoft.Extensions.DependencyInjection;
#endif

namespace ApplicationName.Infrastructure.Persistence;

public static class PersistenceExtensions
{
#if (ef)
    public static void AddPersistence(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(builder => builder.ConfigureDbContext(connectionString));
    }
    #if (sqlServer && postgres)
    private static void ConfigureDbContext(this DbContextOptionsBuilder builder, string connectionString) =>
         builder
             .UseSqlServer(connectionString)
             .UseNpgsql(connectionString)
             .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
             .ConfigureWarnings(x => x.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
    #elif (sqlServer)
    private static void ConfigureDbContext(this DbContextOptionsBuilder builder, string connectionString) =>
         builder
             .UseSqlServer(connectionString)
             .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
             .ConfigureWarnings(x => x.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
    #elif (postgres)
    private static void ConfigureDbContext(this DbContextOptionsBuilder builder, string connectionString) =>
         builder
             .UseNpgsql(connectionString)
             .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
             .ConfigureWarnings(x => x.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
    #else
    private static void ConfigureDbContext(this DbContextOptionsBuilder builder) =>
         builder
             .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
             .ConfigureWarnings(x => x.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
    #endif
#elif (dapper)
    #if (sqlServer && postgres)
    public static void AddPersistence(this IServiceCollection services, string connectionString)
    {
        services.AddTransient<IDbConnection>(_ => new SqlConnection(connectionString));
        services.AddTransient<IDbConnection>(_ => new NpgsqlConnection(connectionString));
    }
    #elif (sqlServer)
    public static void AddPersistence(this IServiceCollection services, string connectionString)
    {
        services.AddTransient<IDbConnection>(_ => new SqlConnection(connectionString));
    }
    #elif (postgres)
    public static void AddPersistence(this IServiceCollection services, string connectionString)
    {
        services.AddTransient<IDbConnection>(_ => new NpgsqlConnection(connectionString));
    }
    #else
    public static void AddPersistence(this IServiceCollection services, string connectionString)
    {
        services.AddTransient<IDbConnection>(_ => /*Your DB Connection here*/);
    }
    #endif
#else
    public static void AddPersistence(this IServiceCollection services)
    {
    }
#endif
}