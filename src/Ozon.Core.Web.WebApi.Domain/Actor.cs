using Ozon.Core.Web.WebApi.Domain.Abstractions;
using Ozon.Core.Web.WebApi.Domain.ValueObjects;

namespace Ozon.Core.Web.WebApi.Domain;

public sealed class Actor : DomainObject, IEquatable<Actor>
{
    public Actor(
        string name,
        DateTimeOffset birthDate,
        Gender gender)
        : base()
    {
        Name = name;
        BirthDate = birthDate;
        Gender = gender;
        Rate = new Rate();
    }

    public Actor(
        Guid id,
        string name,
        DateTimeOffset birthDate,
        Gender gender,
        Rate rate)
        : base(id)
    {
        Name = name;
        BirthDate = birthDate;
        Gender = gender;
        Rate = rate;
    }

    public string Name { get; }

    public DateTimeOffset BirthDate { get; }

    public Gender Gender { get; }

    public Rate Rate { get; private set; }

    public void AddRate(Rate rate)
    {
        Rate += rate;
    }

    public bool Equals(Actor? other)
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
            && string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase)
            && BirthDate.Equals(other.BirthDate)
            && Gender == other.Gender
            && Rate.Equals(other.Rate);
    }

    public override bool Equals(object? obj)
    {
        return obj is Actor other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, BirthDate, (int)Gender, Rate);
    }
}