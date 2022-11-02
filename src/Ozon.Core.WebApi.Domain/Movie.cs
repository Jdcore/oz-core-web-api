using Ozon.Core.WebApi.Domain.Abstractions;
using Ozon.Core.WebApi.Domain.ValueObjects;

namespace Ozon.Core.WebApi.Domain;

public sealed class Movie : DomainObject, IEquatable<Movie>
{
    private readonly List<Actor> _actors;

    public string Name { get; }

    public int ProductionYear { get; }

    public IEnumerable<Actor> Actors => _actors;

    public Movie(string name, int year)
        : base()
    {
        Name = name;
        ProductionYear = year;
        _actors = new List<Actor>();
    }

    public Movie(Guid id, string name, int year, IEnumerable<Actor> actors)
        : base(id)
    {
        Name = name;
        ProductionYear = year;
        _actors = actors.ToList();
    }

    public void AddActor(Actor actor)
    {
        if (_actors.Any(a => a.Equals(actor)))
        {
            return;
        }
        _actors.Add(actor);
    }

    public void RemoveActor(Actor actor)
    {
        _actors.RemoveAll(a => a.Equals(actor));
    }

    public Rate GetRate()
    {
        int sum = _actors.Sum(x => x.Rate.Value);
        int count = _actors.Count();

        return new Rate(sum, count);
    }

    public bool Equals(Movie? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Id == other.Id
            && _actors.Equals(other._actors)
            && Name == other.Name
            && ProductionYear == other.ProductionYear;
    }

    public override bool Equals(object? obj)
    {
        return obj is Movie other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, _actors, Name, ProductionYear);
    }
}