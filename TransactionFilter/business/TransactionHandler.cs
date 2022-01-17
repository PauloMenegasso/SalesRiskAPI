using TransactionFilter.domain.transaction;
using TransactionFilter.domain.validators;
using TransactionFilter.infra;

namespace TransactionFilter.business;

public interface ITransactionHandler
{
    public Task<int> InsertOne(TransactionEntry transaction);
    public Task<List<int>> InsertMany(IEnumerable<TransactionEntry> transaction);
}

public class TransactionHandler : ITransactionHandler
{
    private readonly ITransactionRepository transactionRepository;
    private readonly IMerchantHandler merchantHandler;
    private readonly ICardHandler cardHandler;

    public TransactionHandler(ITransactionRepository transactionRepository, IMerchantHandler merchantHandler, ICardHandler cardHandler)
    {
        this.transactionRepository = transactionRepository;
        this.merchantHandler = merchantHandler;
        this.cardHandler = cardHandler;
    }

    public async Task<List<int>> InsertMany(IEnumerable<TransactionEntry> transactions)
    {
        var insertedIds = new List<int>();
        if (!transactions.Any()) return insertedIds;

        foreach (var transaction in transactions)
        {
            var insertedId = await InsertOne(transaction);
            insertedIds.Add(insertedId);
        }

        return insertedIds;
    }

    public async Task<int> InsertOne(TransactionEntry transaction)
    {
        if (!transaction.IsValid) return 0;

        var merchant = await GetMerchant(transaction.Document);

        var card = await GetCard(transaction);

        var sale = new Sale(transaction, merchant, card);

        var saleId = await transactionRepository.InsertAsync(sale);

        return saleId;
    }

    private async Task<Merchant> GetMerchant(string document)
    {
        var merchantId = await merchantHandler.GetMerchant(document);

        if (merchantId == 0)
        {
            merchantId = await merchantHandler.InsertMerchant(document);
        }

        return new Merchant(merchantId, document);
    }

    private async Task<Card> GetCard(TransactionEntry transaction)
    {
        var cardId = await cardHandler.GetCard(transaction);

        if (cardId == 0)
        {
            cardId = await cardHandler.InsertCard(transaction);
        }

        return new Card(cardId, transaction.CardNumber, transaction.TransactionDateTime);
    }
}

