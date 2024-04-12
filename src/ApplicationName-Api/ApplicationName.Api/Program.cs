using ApplicationName.Api.Extensions;
using ApplicationName.Api.Middlewares;
using ApplicationName.Application;
using ApplicationName.Infrastructure.Persistence;
using ApplicationName.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.
#if ((ef || dapper) && postgres)
builder.Services.AddPersistence(builder.Configuration.GetValue<string>("ConnectionStrings:Postgres"));
#elif ((ef || dapper) && sqlServer)
builder.Services.AddPersistence(builder.Configuration.GetValue<string>("ConnectionStrings:Sql"));
#else
builder.Services.AddPersistence();
#endif
builder.Services.AddRepositories();
builder.Services.AddApplication();
builder.Services.AddApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
#if (Docker)
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Docker"))
{
    app.UseDeveloperExceptionPage();
}
#else
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
#endif

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseRouting();
app.UseCors(builder.Configuration.GetValue<string>("AllowedOrigin"));
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();

#if HealthCheck
app.UseHealthChecks("HEALTHCHECK-PATH");
#endif

app.MapControllers();
app.MapGet("/", () => "ApplicationName Api is alive and kicking!");

app.Run();