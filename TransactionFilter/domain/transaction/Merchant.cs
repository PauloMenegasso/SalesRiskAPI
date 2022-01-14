namespace TransactionFilter.domain.transaction;
public class Merchant
{
    public int MerchantId { get; set; }
    public string MerchantDocument { get; set; }
    public long MeanTicket { get; set; }
    public long ThirtyDayMeanTicket { get; set; }

    public Merchant(int merchantId, string document)
    {
        MerchantId = merchantId;
        MerchantDocument = document;
    }
}
