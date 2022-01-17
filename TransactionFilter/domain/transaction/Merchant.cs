using Dapper.Contrib.Extensions;

namespace TransactionFilter.domain.transaction;
[Table("Sale.Merchant")]
public class Merchant
{
    [Key]
    public int MerchantId { get; set; }
    public string Document { get; set; }
    public long MeanTicket { get; set; }
    public long ThirtyDaysMeanTicket { get; set; }

    private Merchant()
    {
        this.Document = "";
    }
    public Merchant(int merchantId, string document)
    {
        MerchantId = merchantId;
        Document = document;
    }

    public Merchant(string document)
    {
        Document = document;
    }
}
