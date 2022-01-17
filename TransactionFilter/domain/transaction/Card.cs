using Dapper.Contrib.Extensions;

namespace TransactionFilter.domain.transaction;

[Table("Sale.Card")]
public class Card
{
    private DateTime transactionDateTime;

    [Key]
    public int CardId { get; set; }
    public string CardNumber { get; set; }
    public DateTime LastUsed { get; set; }
    private Card() { }

    public Card(int cardId, string cardNumber, DateTime transactionDateTime)
    {
        CardId = cardId;
        CardNumber = cardNumber;
        LastUsed = transactionDateTime;
    }

    public Card(string cardNumber, DateTime transactionDateTime)
    {
        CardNumber = cardNumber;
        this.LastUsed = transactionDateTime;
    }
}
