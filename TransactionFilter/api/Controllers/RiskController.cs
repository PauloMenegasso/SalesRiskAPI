using Microsoft.AspNetCore.Mvc;
using TransactionFilter.business;
using TransactionFilter.domain.transaction;

namespace TransactionFilter.api.Controllers;
[ApiController]
[Route("[controller]")]
public class RiskController : ControllerBase
{
    private readonly IRiskHandler riskHandler;
    private readonly ILogger<RiskController> logger;

    public RiskController(IRiskHandler riskHandler, ILogger<RiskController> logger)
    {
        this.riskHandler = riskHandler;
        this.logger = logger;
    }

    [HttpPost("analyseSale")]
    public async Task<SaleReport> AnalyseSale([FromBody] TransactionEntry transaction)
    {
        var result = await this.riskHandler.AnalyseSale(transaction);
        return result;
    }
}
