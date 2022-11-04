using Microsoft.Extensions.DependencyInjection;

using Ozon.Core.WebApi.Abstractions.Repositories;

namespace Ozon.Core.WebApi.Infrastructure.Repositories;

public static class InfraRegistrar
{
    public static IServiceCollection ConfigurePostgres(this IServiceCollection services)
    {
        services.AddSingleton<IActorRepository, NpgsqlRepository>();
        services.AddSingleton<IMovieRepository, NpgsqlRepository>();

        return services;
    }
}