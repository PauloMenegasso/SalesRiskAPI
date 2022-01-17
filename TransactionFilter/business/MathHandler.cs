using TransactionFilter.domain.transaction;
using TransactionFilter.infra;

namespace TransactionFilter.business;

public interface IMathHandler
{
    Task CalculateOrUpdateMean();
}

public class MathHandler : IMathHandler
{
    private readonly ITransactionRepository transactionRepository;
    private readonly IMerchantRepository merchantRepository;

    public MathHandler(ITransactionRepository transactionRepository, IMerchantRepository merchantRepository)
    {
        this.transactionRepository = transactionRepository;
        this.merchantRepository = merchantRepository;
    }
    public async Task CalculateOrUpdateMean()
    {
        var merchants = await merchantRepository.GetAllMerchants();

        foreach (var merchant in merchants)
        {
            var sales = await transactionRepository.GetSalesByMerchantID(merchant.MerchantId);

            merchant.MeanTicket = Convert.ToInt64(CalculateMeanTicket(sales));
            merchant.ThirtyDaysMeanTicket = Convert.ToInt64(CalculateMeanTicket(sales, true));

            await merchantRepository.UpdateAsync(merchant);
        }
    }

    private double CalculateMeanTicket(List<Sale> sales, bool isLastThirtyDays = false)
    {
        var validSales = new List<Sale>();

        if (isLastThirtyDays)
        {
            validSales.AddRange(sales.Where(s => s.SaleDateTime > DateTime.UtcNow.AddDays(-30)));
        }
        else
        {
            validSales.AddRange(sales);
        }

        var numberOfSales = (double)validSales.Count;

        var totalAmount = (double)validSales.Sum(s => s.Amount);

        return numberOfSales != 0 ? totalAmount / numberOfSales : 0;
    }
}
