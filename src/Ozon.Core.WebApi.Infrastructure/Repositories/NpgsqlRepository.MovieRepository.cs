using Ozon.Core.WebApi.Abstractions.Repositories;
using Ozon.Core.WebApi.Domain;

namespace Ozon.Core.WebApi.Infrastructure.Repositories;

internal partial class NpgsqlRepository : IMovieRepository
{
    async Task<Movie> IMovieRepository.Get(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    async Task<IEnumerable<Movie>> IMovieRepository.Find(string filter, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    async Task IMovieRepository.Save(Movie movie, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    async Task IMovieRepository.Delete(Movie movie, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}