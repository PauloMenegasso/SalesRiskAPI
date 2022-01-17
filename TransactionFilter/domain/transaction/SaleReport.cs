namespace TransactionFilter.domain.transaction;
public class SaleReport
{
    public int RiskScore { get; set; }
    public List<string> BlockReasons { get; set; }
    public string RiskLevel { get; set; }

    public SaleReport(int riskScore, List<string> blockReasons)
    {
        BlockReasons = new List<string>();
        RiskScore = riskScore;
        BlockReasons.AddRange(blockReasons);
        if (riskScore < 30) { RiskLevel = "Low Risk"; }
        if (riskScore >= 30 && riskScore < 50) { RiskLevel = "Medium Risk"; }
        if (riskScore >= 50 && riskScore < 100) { RiskLevel = "High Risk"; }
        if (riskScore >= 100) { RiskLevel = "Very High Risk"; }
    }
}
