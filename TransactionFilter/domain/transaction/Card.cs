namespace TransactionFilter.domain.transaction;
public class Card
{
    public int CardId { get; set; }
    public string CardNumber { get; set; }
    public DateTime LastUsed { get; set; }

    public Card(int cardId, string cardNumber, DateTime transactionDateTime)
    {
        CardId = cardId;
        CardNumber = cardNumber;
        LastUsed = transactionDateTime;
    }

}
