using Ozon.Core.WebApi.Domain;
using Ozon.Core.WebApi.Presentation.Models;

namespace Ozon.Core.WebApi.Presentation.Converters;

internal static class ActorConverter
{
    public static ActorModel ToModel(this Actor source)
    {
        return new ActorModel()
        {
            Id = source.Id,
            Name = source.Name,
            BirthDate = source.BirthDate,
            Gender = source.Gender.ToModel(),
            Rate = source.Rate.Value
        };
    }

    public static Actor ToDomain(this ActorCreateModel source)
    {
        return new Actor(
            source.Name!,
            source.BirthDate,
            source.Gender!.ToDomain());
    }
}