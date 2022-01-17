using TransactionFilter.domain.transaction;
using TransactionFilter.infra;

namespace TransactionFilter.business;

public interface IMerchantHandler
{
    Task<int> InsertMerchant(string document);
    Task<int> GetMerchant(string document);

}
public class MerchantHandler : IMerchantHandler
{
    private readonly IMerchantRepository merchantRepository;

    public MerchantHandler(IMerchantRepository merchantRepository)
    {
        this.merchantRepository = merchantRepository;
    }
    public async Task<int> GetMerchant(string document)
    {
        var merchant = await merchantRepository.GetMerchantByDocument(document);

        return merchant is not null ? merchant.MerchantId : 0;
    }

    public async Task<int> InsertMerchant(string document)
    {
        var merchant = new Merchant(document);
        try
        {
            return await merchantRepository.InsertAsync(merchant);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}
