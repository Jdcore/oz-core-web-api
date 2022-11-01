namespace Ozon.Core.WebApi.Presentation.Models;

public sealed class ActorCreateModel
{
    public string? Name { get; set; }

    public DateTime BirthDate { get; set; }

    public string? Gender { get; set; }
}