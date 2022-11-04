using Ozon.Core.WebApi.Domain;
using Ozon.Core.WebApi.Domain.ValueObjects;
using Ozon.Core.WebApi.Infrastructure.Dtos;

namespace Ozon.Core.WebApi.Infrastructure.Converters;

internal static class ActorConverter
{
    public static Actor ToDomain(this ActorDto dto)
    {
        var rate = new Rate(dto.SumOfRate, dto.CountOfRate);

        return new Actor(
            dto.Id,
            dto.Name!,
            dto.BirthDate,
            (Gender)dto.Gender,
            rate);
    }
}