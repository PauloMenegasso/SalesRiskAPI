using Dapper.Contrib.Extensions;

namespace TransactionFilter.domain.transaction;
[Table("Sale.Sale")]
public class Sale
{


    [Key]
    [Computed]
    public long SaleId { get; set; }
    public long Amount { get; set; }
    public int MerchantId { get; set; }
    public long CardId { get; set; }
    public DateTime SaleDateTime { get; set; }
    public DateTime CreatedAt { get; set; }

    public Sale(TransactionEntry transaction, Merchant merchant, Card card)
    {
        this.Amount = transaction.Amount;
        this.MerchantId = merchant.MerchantId;
        this.CardId = card.CardId;
        this.SaleDateTime = transaction.TransactionDateTime;
        this.CreatedAt = DateTime.UtcNow;
    }

    private Sale() { }
    //public string Status { get; set; } inserir aprovadas e reprovadas??
}
