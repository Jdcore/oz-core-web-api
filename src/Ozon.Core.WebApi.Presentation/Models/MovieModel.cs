namespace Ozon.Core.WebApi.Presentation.Models;

public sealed class MovieModel
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public int Year { get; set; }
}