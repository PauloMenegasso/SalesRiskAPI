using Dapper;
using TransactionFilter.domain.transaction;

namespace TransactionFilter.infra;
public interface ICardRepository : IBaseRepository
{
    Task<Card> GetCardByNumber(string cardNumber);
}

public class CardRepository : BaseRepository, ICardRepository
{
    public CardRepository(IDbProvider dbProvider) : base(dbProvider)
    {
        sqlConnection.Open();
    }

    public async Task<Card> GetCardByNumber(string cardNumber)
    {
        return await sqlConnection.QueryFirstOrDefaultAsync<Card>($"SELECT * FROM [SALE].[CARD] WHERE CardNumber = {cardNumber}");
    }
}
