using Dapper;
using TransactionFilter.domain.transaction;

namespace TransactionFilter.infra;
public interface ITransactionRepository : IBaseRepository
{
    Task<List<Sale>> GetSalesByMerchantID(int merchantId);
    Task<Sale> GetLastMerchantTransaction(int merchantId);
    Task<List<Sale>> GetLastTwentyCardSales(int cardId);
    Task<List<Sale>> GetLastFiveCardSales(int cardId);
}

public class TransactionRepository : BaseRepository, ITransactionRepository
{
    public TransactionRepository(IDbProvider dbProvider) : base(dbProvider)
    {
        sqlConnection.Open();
    }

    public async Task<List<Sale>> GetLastFiveCardSales(int cardId)
    {
        var sales = await sqlConnection.QueryAsync<Sale>($"SELECT TOP 5 * FROM SALE.SALE WHERE CardId = '{cardId}' ORDER BY 1 DESC");
        return sales.ToList();
    }


    public async Task<List<Sale>> GetLastTwentyCardSales(int cardId)
    {
        var sales = await sqlConnection.QueryAsync<Sale>($"SELECT TOP 20 * FROM SALE.SALE WHERE CardId = '{cardId}' ORDER BY 1 DESC");
        return sales.ToList();
    }
    public async Task<Sale> GetLastMerchantTransaction(int merchantId)
    {
        return await sqlConnection.QueryFirstOrDefaultAsync<Sale>($"SELECT * FROM SALE.SALE WHERE MerchantId = '{merchantId}' ORDER BY 1 DESC");
    }

    public async Task<List<Sale>> GetSalesByMerchantID(int merchantId)
    {
        var sales = await sqlConnection.QueryAsync<Sale>($"SELECT * FROM SALE.SALE WHERE MerchantId = '{merchantId}'");
        return sales.ToList();
    }
}
