using TransactionFilter.domain.transaction;
using TransactionFilter.domain.validators;
using TransactionFilter.infra;

namespace TransactionFilter.business;

public interface ITransactionHandler
{
    public Task<int> InsertOne(Transaction transaction); 
    public Task<List<int>> InsertMany(IEnumerable<Transaction> transaction);
}

public class TransactionHandler : ITransactionHandler
{
    private readonly ITransactionRepository transactionRepository;

    public TransactionHandler(ITransactionRepository transactionRepository)
    {
        this.transactionRepository = transactionRepository;
    }

    public async Task<List<int>> InsertMany(IEnumerable<Transaction> transactions)
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

    public async Task<int> InsertOne(Transaction transaction)
    {
        if (!transaction.IsVaid) return 0;

        var transactionId = await transactionRepository.InsertOne(transaction);

        return transactionId;
    }
}

