using Npgsql;

namespace Ozon.Core.WebApi.Infrastructure.Repositories;

internal sealed partial class NpgsqlRepository
{
    private static string ConnectionString = "Server=localhost;Database=postgres;User Id=postgres;Password=mypwd;";

    private NpgsqlConnection CreateConnection()
    {
        return new NpgsqlConnection(ConnectionString);
    }
}