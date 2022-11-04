namespace Ozon.Core.WebApi.Infrastructure.Dtos;

internal sealed class ActorDto
{
    public Guid Id { get; set; }

    public string? Name { get; set;  }

    public DateTimeOffset BirthDate { get; set; }

    public int Gender { get; set; }

    public int SumOfRate { get; set; }

    public int CountOfRate { get; set; }
}