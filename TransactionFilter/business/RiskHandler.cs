using TransactionFilter.domain.transaction;
using TransactionFilter.infra;

namespace TransactionFilter.business;

public interface IRiskHandler
{
    Task<SaleReport> AnalyseSale(TransactionEntry transaction);
}
public class RiskHandler : IRiskHandler
{
    private readonly IMerchantRepository merchantRepository;
    private readonly ICardRepository cardRepository;
    private readonly ITransactionRepository transactionRepository;

    public RiskHandler(IMerchantRepository merchantRepository, ICardRepository cardRepository, ITransactionRepository transactionRepository)
    {
        this.merchantRepository = merchantRepository;
        this.cardRepository = cardRepository;
        this.transactionRepository = transactionRepository;
    }
    public async Task<SaleReport> AnalyseSale(TransactionEntry transaction)
    {
        if (!transaction.IsValid) { return new SaleReport(0, new List<string> { "Transaction is not valid" }); }

        var merchant = await merchantRepository.GetMerchantByDocument(transaction.Document);
        var card = await cardRepository.GetCardByNumber(transaction.CardNumber);

        if (merchant is null || card is null) { return new SaleReport(0, new List<string> { "Merchant of card is not in the database" }); }
        if (merchant.MeanTicket == 0) { return new SaleReport(0, new List<string> { "Merchant has no mean ticket yet. Calculate mean first" }); }

        var transactionScore = 0;
        var riskWarnings = new List<string>();

        var (scoreAddFromMerchantValue, ratioMeanTicket) = AnalyseSaleMerchantValue(transaction, merchant);
        if (scoreAddFromMerchantValue > 0) { riskWarnings.Add($"This transaction is {ratioMeanTicket} higher than avarage merchant transaction mean value"); }

        var (scoreAddFromMerchantBehavior, warningMessage) = await AnalyseMerchantBehavior(transaction.TransactionDateTime, merchant.MerchantId);
        if (scoreAddFromMerchantBehavior > 0) { riskWarnings.Add(warningMessage); }

        var (scoreAddByCardLastTransaction, warningCardLastTransactionMessage) = AnalyseCardLastTransaction(card, transaction);
        if (scoreAddByCardLastTransaction > 0) { riskWarnings.Add(warningCardLastTransactionMessage); }

        var (scoreAddByMultipleTentatives, warningCardMultipleTentatives) = await AnalyseCardMultipleTentatives(card, transaction);
        if (scoreAddByMultipleTentatives > 0) { riskWarnings.Add(warningCardMultipleTentatives); }

        var (scoreAddByCardMeanTicket, warningCardMeanTicket) = await AnalyseCardLastTransactionsMeanTicket(card, transaction.Amount);
        if (scoreAddByCardMeanTicket > 0) { riskWarnings.Add(warningCardMeanTicket); }

        var (scoreAddByIncreasingValueTentatives, warningIncresingValueTentatives) = await AnalyseCardByIncreasingValuesTentatives(card.CardId);
        if (scoreAddByIncreasingValueTentatives > 0) { riskWarnings.Add(warningIncresingValueTentatives); }

        transactionScore = scoreAddFromMerchantValue + scoreAddFromMerchantBehavior + scoreAddByCardLastTransaction + scoreAddByMultipleTentatives + scoreAddByCardMeanTicket + scoreAddByIncreasingValueTentatives;
        return new SaleReport(transactionScore, riskWarnings);
    }

    private async Task<(int, string)> AnalyseCardByIncreasingValuesTentatives(int cardId)
    {
        var transactions = (await transactionRepository.GetLastFiveCardSales(cardId)).OrderBy(t => t.SaleDateTime);

        var increasingValues = false;
        var firstValue = transactions.FirstOrDefault().Amount;

        foreach (var transaction in transactions)
        {
            increasingValues = transaction.Amount >= firstValue;
            firstValue = transaction.Amount;
        }

        return increasingValues ? (50, "Increasing values transactions has been detected") : (0, "");
    }

    private async Task<(int, string)> AnalyseCardLastTransactionsMeanTicket(Card card, long amount)
    {
        var transactions = await transactionRepository.GetLastTwentyCardSales(card.CardId);

        var meanValue = transactions.Average(t => t.Amount);

        var ratio = amount / meanValue;

        if (ratio > 2) { return (20, "Transaction amount is more than twice higher than average card amount"); }
        if (ratio > 1) { return (5, "Transaction amount is higher than average card amount"); }
        return (0, "");
    }

    private async Task<(int, string)> AnalyseCardMultipleTentatives(Card card, TransactionEntry transaction)
    {
        var transactions = await transactionRepository.GetLastTwentyCardSales(card.CardId);

        var numberOfLastDayTransactions = transactions.Where(t => t.SaleDateTime > transaction.TransactionDateTime.AddDays(-1)).Count();

        if (numberOfLastDayTransactions > 5) { return (50, "This card has more than 5 transactions since yesterday"); }
        if (numberOfLastDayTransactions > 2) { return (10, "A few transactions have been detected for this card"); }
        return (0, "");
    }

    private (int, string) AnalyseCardLastTransaction(Card card, TransactionEntry transaction)
    {
        var timeSinceLastUsage = transaction.TransactionDateTime - card.LastUsed;

        return timeSinceLastUsage.Days >= 60
            ? (10, $"The card has not been used in the last {timeSinceLastUsage.Days} days")
            : (0, "");
    }

    private async Task<(int, string)> AnalyseMerchantBehavior(DateTime transactionDateTime, int merchantId)
    {
        var lastTransaction = await transactionRepository.GetLastMerchantTransaction(merchantId);

        var dateDifference = transactionDateTime - lastTransaction.SaleDateTime;

        if (dateDifference.Days >= 90) { return (20, $"Merchant didn't transactioned for {dateDifference.Days} days"); }
        if (dateDifference.Days >= 30) { return (10, $"Merchant didn't transactioned for {dateDifference.Days} days"); }
        return (0, "");
    }

    private (int, double) AnalyseSaleMerchantValue(TransactionEntry transaction, Merchant merchant)
    {
        var addedScore = 0;

        var ratioMeanTicket = transaction.Amount / (double)merchant.MeanTicket;

        if (ratioMeanTicket > 1) { addedScore += 10; }
        if (ratioMeanTicket >= 1.5) { addedScore += 10; }
        if (ratioMeanTicket >= 2) { addedScore += 10; }
        if (ratioMeanTicket >= 3) { addedScore += 10; }
        if (ratioMeanTicket >= 5) { addedScore += 10; }

        return (addedScore, ratioMeanTicket);
    }
}
