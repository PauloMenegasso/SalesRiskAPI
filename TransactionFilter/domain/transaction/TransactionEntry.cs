using TransactionFilter.domain.validators;

namespace TransactionFilter.domain.transaction;
public class TransactionEntry
{
    private static readonly TransactionValidator Validator = new();
    public long Amount { get; set; }
    public string Document { get; set; }
    public string CardNumber { get; set; }
    public DateTime TransactionDateTime { get; set; }
    public bool IsValid => Validator.Validate(this).IsValid;
}

