using Ozon.Core.WebApi.Domain;

namespace Ozon.Core.WebApi.Abstractions.Repositories;

public interface IActorRepository
{
    Task<Actor> Get(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<Actor>> Find(string filter, CancellationToken cancellationToken);

    Task Save(Actor actor, CancellationToken cancellationToken);
}