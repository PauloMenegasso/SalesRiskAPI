using TransactionFilter.domain.transaction;

namespace TransactionFilter.business;

public interface IMerchantHandler
{
    Task<int> InsertMerchant(string document);
    Task<int> GetMerchant(string document);

}
public class MerchantHandler : IMerchantHandler
{
    public async Task<int> GetMerchant(string document)
    {
        throw new NotImplementedException();
    }

    public async Task<int> InsertMerchant(string document)
    {
        throw new NotImplementedException();
    }
}
