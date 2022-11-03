using Ozon.Core.WebApi.Domain;
using Ozon.Core.WebApi.Presentation.Models;

namespace Ozon.Core.WebApi.Presentation.Converters;

internal static class MovieConverter
{
    public static MovieModel ToModel(this Movie source)
    {
        return new MovieModel()
        {
            Id = source.Id,
            Name = source.Name!,
            Year = source.ProductionYear,
            Actors = source.Actors.Select(s => s.Name).ToArray()
        };
    }

    public static Movie ToDomain(this MovieCreateModel source)
    {
        return new Movie(source.Name!, source.Year);
    }
}