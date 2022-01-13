using TransactionFilter.domain.validators;

namespace TransactionFilter.domain.transaction;
public class Transaction
{
    private static readonly TransactionValidator Validator = new();
    public long TransactionId { get; set; }
    public long Amount { get; set; }    
    public string Document { get; set; }
    public string CardNumber { get; set; }
    public DateTime TransactionDateTime { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; }

    public bool IsVaid => Validator.Validate(this).IsValid;
}
