using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using TransactionFilter.domain.transaction;

namespace TransactionFilter.infra;
public interface ITransactionRepository
{
    public Task<int> InsertOne(Transaction transaction);
}

public class TransactionRepository : ITransactionRepository
{
    private readonly string connectionString;
    private readonly SqlConnection connection;

    public TransactionRepository(IConfiguration configuration)
    {
        this.connectionString = configuration["ConnectionString"];
        this.connection = new SqlConnection(connectionString);
        connection.Open();
    }

    public async Task<int> InsertOne(Transaction transaction)
    {
        return await connection.InsertAsync(transaction);
    }
}
