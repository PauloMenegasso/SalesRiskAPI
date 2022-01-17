using TransactionFilter.domain.transaction;
using TransactionFilter.infra;

namespace TransactionFilter.business;

public interface ICardHandler
{
    Task<int> InsertCard(TransactionEntry transaction);
    Task<int> GetCard(TransactionEntry transaction);
}

public class CardHandler : ICardHandler
{
    private readonly ICardRepository cardRepository;

    public CardHandler(ICardRepository cardRepository)
    {
        this.cardRepository = cardRepository;
    }
    public async Task<int> GetCard(TransactionEntry transaction)
    {
        var card = await cardRepository.GetCardByNumber(transaction.CardNumber);

        return card is not null ? card.CardId : 0;
    }

    public async Task<int> InsertCard(TransactionEntry transaction)
    {
        var card = new Card(transaction.CardNumber, transaction.TransactionDateTime);
        return await cardRepository.InsertAsync(card);
    }
}
