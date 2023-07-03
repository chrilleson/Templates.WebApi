using System.Text.Json;

namespace ApplicationName.Common.Json;

public class CustomJsonNamingPolicy : JsonNamingPolicy
{
    private static readonly IDictionary<char, char> Replacements = new Dictionary<char, char>
    {
        { 'å', 'a' },
        { 'ä', 'a' },
        { 'ö', 'o' },
        { 'Å', 'A' },
        { 'Ä', 'A' },
        { 'Ö', 'O' },
    };

    public override string ConvertName(string name) =>
        ReplaceDiacritics(name);

    private static string ReplaceDiacritics(string propertyName) =>
        propertyName.Aggregate(string.Empty, (a, b) => a + (Replacements.TryGetValue(b, out var value) ? value : b));
}