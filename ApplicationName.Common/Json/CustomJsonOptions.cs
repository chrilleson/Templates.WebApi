using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApplicationName.Common.Json;

public class CustomJsonOptions
{
    private static readonly JsonSerializerOptions _options = Configure(new JsonSerializerOptions());

    public static JsonSerializerOptions Value => _options;

    public static JsonSerializerOptions Configure(JsonSerializerOptions options)
    {
        options.PropertyNamingPolicy = new CustomJsonNamingPolicy();
        options.Converters.Add(new JsonStringEnumConverter());

        return options;
    }
}