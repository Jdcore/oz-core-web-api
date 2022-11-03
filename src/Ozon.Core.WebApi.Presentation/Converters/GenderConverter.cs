using Ozon.Core.WebApi.Domain.ValueObjects;

namespace Ozon.Core.WebApi.Presentation.Converters;

internal static class GenderConverter
{
    public static string ToModel(this Gender source)
    {
        return source switch
        {
            Gender.Man => "M",
            Gender.Woman => "W",
            _ => string.Empty
        };
    }

    public static Gender ToDomain(this string source)
    {
        return source switch
        {
            "M" => Gender.Man,
            "W" => Gender.Woman,
            _ => Gender.None
        };
    }
}