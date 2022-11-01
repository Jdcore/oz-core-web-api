namespace Ozon.Core.WebApi.Presentation.Models;

public sealed class ActorModel
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public DateTimeOffset BirthDate { get; set; }

    public string? Gender { get; set; }

    public int Rate { get; set; }
}