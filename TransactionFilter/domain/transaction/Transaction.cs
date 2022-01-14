using Dapper.Contrib.Extensions;

namespace TransactionFilter.domain.transaction;
[Table("Transaction.Transactio")]
public class Transaction
{
    
    [Key]
    [Computed]
    public long TransactionId { get; set; }
    public long Amount { get; set; }
    public int MerchantId { get; set; }
    public long CardId { get; set; }
    public DateTime TransactionDateTime { get; set; }
    public DateTime CreatedAt { get; set; }
    //public string Status { get; set; } inserir aprovadas e reprovadas??

}
