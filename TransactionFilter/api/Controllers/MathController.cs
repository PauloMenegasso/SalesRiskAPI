using Microsoft.AspNetCore.Mvc;
using TransactionFilter.business;

namespace TransactionFilter.api.Controllers;

[ApiController]
[Route("[controller]")]
public class MathController : ControllerBase
{
    private readonly IMathHandler mathHandler;
    private readonly ILogger<MathController> logger;

    public MathController(ILogger<MathController> logger, IMathHandler mathHandler)
    {
        this.logger = logger;
        this.mathHandler = mathHandler;
    }

    [HttpGet("calculatemean")]
    public async Task<ActionResult> CalculateMean()
    {
        await mathHandler.CalculateOrUpdateMean();
        return Ok();
    }
}
