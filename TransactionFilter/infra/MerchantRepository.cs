using Dapper;
using TransactionFilter.domain.transaction;

namespace TransactionFilter.infra;
public interface IMerchantRepository : IBaseRepository
{
    Task<Merchant> GetMerchantByDocument(string document);
    Task<List<Merchant>> GetAllMerchants();
}

public class MerchantRepository : BaseRepository, IMerchantRepository
{
    public MerchantRepository(IDbProvider dbProvider) : base(dbProvider)
    {
        sqlConnection.Open();
    }

    public async Task<List<Merchant>> GetAllMerchants()
    {
        var collection = await sqlConnection.QueryAsync<Merchant>("SELECT * FROM [SALE].[MERCHANT]");
        return collection.ToList();
    }

    public async Task<Merchant> GetMerchantByDocument(string document)
    {
        try
        {

            var merchant = await sqlConnection.QueryFirstOrDefaultAsync<Merchant>($"SELECT * FROM [SALE].[MERCHANT] WHERE Document = '{document}'");
            return merchant;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}
