using System.Text.Json;
using ApplicationName.Common.Json;
using FluentAssertions;

namespace ApplicationName.Tests.Common.Json;

public class CustomJsonNamingPolicyTest
{
    [Fact]
    public void Serialize_ObjectWithDiacritics_ShouldSerializeToJsonWithoutDiacritics()
    {
        var obj = new CustomJsonObject(1, 2, 3, 4, 5);

        var actual = JsonSerializer.Serialize(obj, new JsonSerializerOptions{ PropertyNamingPolicy = new CustomJsonNamingPolicy() });

        actual.Should().Be("""{ "Normal":1, "ÅIBörjan":2, "InnehållerÄ":3, "FortsätterMedÖ":4, "gemenIBörjan":5 }""");
    }

    private record CustomJsonObject(int Normal, int ÅIBörjan, int InnehållerÄ, int FortsätterMedÖ, int gemenIBörjan);
}