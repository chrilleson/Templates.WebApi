using ApplicationName.Api.Extensions;
using ApplicationName.Api.Middlewares;
using ApplicationName.Application;
using ApplicationName.Infrastructure.Persistence;
using ApplicationName.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddPersistence();
builder.Services.AddRepositories();
builder.Services.AddApplication();
builder.Services.AddApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Docker"))
{
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

#if HealthCheck
app.UseHealthChecks("HEALTHCHECK-PATH");
#endif

app.Run();