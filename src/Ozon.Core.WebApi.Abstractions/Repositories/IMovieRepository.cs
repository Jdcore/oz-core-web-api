using Ozon.Core.WebApi.Domain;

namespace Ozon.Core.WebApi.Abstractions.Repositories;

public interface IMovieRepository
{
    Task<Movie> Get(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<Movie>> Find(string filter, CancellationToken cancellationToken);

    Task Save(Movie movie, CancellationToken cancellationToken);

    Task Delete(Movie movie, CancellationToken cancellationToken);
}