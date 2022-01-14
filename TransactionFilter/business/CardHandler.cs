using TransactionFilter.domain.transaction;

namespace TransactionFilter.business;

public interface ICardHandler
{
    Task<int> InsertCard(TransactionEntry transaction);
    Task<int> GetCard(TransactionEntry transaction);
}

public class CardHandler : ICardHandler
{
    public Task<int> GetCard(TransactionEntry transaction)
    {
        throw new NotImplementedException();
    }

    public Task<int> InsertCard(TransactionEntry transaction)
    {
        throw new NotImplementedException();
    }
}
