using Microsoft.AspNetCore.Mvc;
using TransactionFilter.business;
using TransactionFilter.domain.transaction;

namespace TransactionFilter.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionHandler transactionHandler;
    private readonly ILogger<TransactionController> _logger;

    public TransactionController(ILogger<TransactionController> logger, ITransactionHandler transactionHandler)
    {
        _logger = logger;
        this.transactionHandler = transactionHandler;
    }

    [HttpPost("insert")]
    public async Task<ActionResult> InsertOne([FromBody] TransactionEntry transaction)
    {
        await transactionHandler.InsertOne(transaction);
        return Ok();
    }

    [HttpPost("insertMany")]
    public async Task<ActionResult> InsertMany([FromBody] IEnumerable<TransactionEntry> transactions)
    {
        await transactionHandler.InsertMany(transactions);
        return Ok();
    }
}
