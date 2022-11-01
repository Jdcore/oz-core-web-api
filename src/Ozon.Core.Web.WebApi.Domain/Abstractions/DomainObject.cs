namespace Ozon.Core.Web.WebApi.Domain.Abstractions;

public abstract class DomainObject
{
    public Guid Id { get; }

    public bool IsTransient { get; }

    public DomainObject(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException(nameof(id));
        }

        Id = id;
        IsTransient = false;
    }

    public DomainObject()
    {
        Id = Guid.NewGuid();
        IsTransient = true;
    }
}