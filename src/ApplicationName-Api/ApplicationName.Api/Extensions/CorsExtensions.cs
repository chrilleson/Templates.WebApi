namespace ApplicationName.Api.Extensions;

public static class CorsExtensions
{
    public static void UseCors(this WebApplication app, string allowedOrigin) =>
        app.UseCors(builder =>
        {
            builder
                .WithOrigins(allowedOrigin)
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
}