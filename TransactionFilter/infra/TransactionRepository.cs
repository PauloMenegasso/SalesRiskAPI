using Dapper;
using TransactionFilter.domain.transaction;

namespace TransactionFilter.infra;
public interface ITransactionRepository : IBaseRepository
{
    Task<List<Sale>> GetSalesByMerchantID(int merchantId);
}

public class TransactionRepository : BaseRepository, ITransactionRepository
{
    public TransactionRepository(IDbProvider dbProvider) : base(dbProvider)
    {
        sqlConnection.Open();
    }

    public async Task<List<Sale>> GetSalesByMerchantID(int merchantId)
    {
        var sales = await sqlConnection.QueryAsync<Sale>($"SELECT * FROM SALE.SALE WHERE MerchantId = '{merchantId}'");
        return sales.ToList();
    }
}
