using Dapper;

using Npgsql;

using Ozon.Core.WebApi.Abstractions.Repositories;
using Ozon.Core.WebApi.Domain;
using Ozon.Core.WebApi.Domain.Exceptions;
using Ozon.Core.WebApi.Infrastructure.Converters;
using Ozon.Core.WebApi.Infrastructure.Dtos;

namespace Ozon.Core.WebApi.Infrastructure.Repositories;

partial class NpgsqlRepository : IActorRepository
{
    private static readonly string SqlGetPerson = $@"
    select 
        id as {nameof(ActorDto.Id)}, 
        name as {nameof(ActorDto.Name)}, 
        birth_date as {nameof(ActorDto.BirthDate)}, 
        gender as {nameof(ActorDto.Gender)}, 
        rate_sum as {nameof(ActorDto.SumOfRate)}, 
        rate_count as {nameof(ActorDto.CountOfRate)} 
    from actors
    where id = @id
    ";

    private static readonly string SqlFindPerson = $@"
    select 
        id as {nameof(ActorDto.Id)}, 
        name as {nameof(ActorDto.Name)}, 
        birth_date as {nameof(ActorDto.BirthDate)}, 
        gender as {nameof(ActorDto.Gender)}, 
        rate_sum as {nameof(ActorDto.SumOfRate)}, 
        rate_count as {nameof(ActorDto.CountOfRate)} 
    from actors
    where name like @filter
    ";

    private static readonly string SqlCreatePerson = $@"
    insert into actors (id, name, birth_date, gender, rate_sum, rate_count)
    values(@id, @name, @birthDate, @gender, @ratesum, @ratecount)
    ";

    private static readonly string SqlUpdatePerson = $@"
    update actors 
    set rate_sum = @ratesum, 
        rate_count = @ratecount
    where id = @id
    ";

    async Task<Actor> IActorRepository.Get(Guid id, CancellationToken cancellationToken)
    {
        using (NpgsqlConnection connection = CreateConnection())
        {
            await connection.OpenAsync(cancellationToken);

            var dto = await connection.QueryFirstOrDefaultAsync<ActorDto>(
                SqlGetPerson,
                new { id });

            if (dto is null)
            {
                throw new DataNotFoundException();
            }

            return dto.ToDomain();
        }
    }

    async Task<IEnumerable<Actor>> IActorRepository.Find(string filter, CancellationToken cancellationToken)
    {
        using (NpgsqlConnection connection = CreateConnection())
        {
            await connection.OpenAsync(cancellationToken);

            IEnumerable<ActorDto> dtos = await connection.QueryAsync<ActorDto>(
                SqlFindPerson,
                new { filter = $"%{filter}%" });

            return dtos.Select(d => d.ToDomain());
        }
    }

    async Task IActorRepository.Save(Actor actor, CancellationToken cancellationToken)
    {
        var param = new
        {
            id = actor.Id,
            name = actor.Name,
            birthDate = actor.BirthDate.ToUniversalTime(),
            gender = (int)actor.Gender,
            ratesum = actor.Rate.Sum,
            ratecount = actor.Rate.Count
        };

        using (NpgsqlConnection connection = CreateConnection())
        {
            await connection.OpenAsync(cancellationToken);

            using (NpgsqlTransaction transaction = await connection.BeginTransactionAsync(cancellationToken))
            {
                int rowAffected = await connection.ExecuteAsync(
                    actor.IsTransient ? SqlCreatePerson : SqlUpdatePerson,
                    param,
                    transaction);

                if (rowAffected <= 0)
                {
                    await transaction.RollbackAsync(cancellationToken);

                    throw new InvalidOperationException();
                }

                await transaction.CommitAsync(cancellationToken);
            }
        }
    }
}