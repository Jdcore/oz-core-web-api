namespace Ozon.Core.WebApi.Domain.ValueObjects;

public readonly struct Rate : IEquatable<Rate>
{
    public int Sum { get;  }

    public int Count { get; }

    public int Value => Count > 0 ? Sum / Count : 0;

    public Rate(int sum, int count)
    {
        Sum = sum;
        Count = count;
    }

    public Rate()
    {
        Sum = 0;
        Count = 0;
    }

    public static Rate operator +(Rate a, Rate b)
    {
        return new Rate(a.Sum + b.Sum, a.Count + b.Count);
    }

    public bool Equals(Rate other)
    {
        return Sum == other.Sum && Count == other.Count;
    }

    public override bool Equals(object? obj)
    {
        return obj is Rate other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Sum, Count);
    }
}