using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using TransactionFilter.domain.transaction;

namespace TransactionFilter.infra;
public interface ITransactionRepository : IBaseRepository
{
}

public class TransactionRepository : BaseRepository, ITransactionRepository
{
    public TransactionRepository(IDbProvider dbProvider) : base(dbProvider)
    {
        sqlConnection.Open();
    }
}
