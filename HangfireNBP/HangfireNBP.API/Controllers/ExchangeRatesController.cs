using Domain.Entities;
using HangfireNBP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HangfireNBP.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ExchangeRatesController : ControllerBase
{
    private readonly INbpExchangeRateService _exchangeRateService;

    public ExchangeRatesController(INbpExchangeRateService exchangeRateService)
    {
        _exchangeRateService = exchangeRateService;
    }

    [HttpGet("tables")]
    public async Task<IList<ExchangeRateTable>> GetTables()
    {
        var tables = await _exchangeRateService.GetAllTablesWithRatesAsync();
        return tables;
    }
}

