using Microsoft.Data.SqlClient;

namespace TransactionFilter.infra;

public interface IDbProvider
{
    SqlConnection GetConnection();
}

public class DbProvider : IDbProvider
{
    private readonly string connectionString;

    public DbProvider(IConfiguration configuration) => connectionString = configuration["ConnectionString"];
    public SqlConnection GetConnection() => new SqlConnection(connectionString);
}

