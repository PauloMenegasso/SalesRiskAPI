using Microsoft.Data.SqlClient;
using TransactionFilter.infra.configuration;

namespace TransactionFilter.infra;

public interface IDbProvider
{
    SqlConnection GetConnection();
}

public class DbProvider : IDbProvider
{
    private readonly string connectionString;

    public DbProvider(DatabaseConfiguration configuration)
    {
        connectionString = configuration.ConnectionString;
        Console.WriteLine(connectionString);
    }

    public SqlConnection GetConnection() => new SqlConnection(connectionString);
}

