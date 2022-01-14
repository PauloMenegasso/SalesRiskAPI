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
