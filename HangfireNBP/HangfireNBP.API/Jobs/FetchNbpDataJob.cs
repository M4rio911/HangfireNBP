using HangfireNBP.Application.Interfaces;

namespace HangfireNBP.API.Jobs;
public class FetchNbpDataJob
{
    private readonly INbpExchangeRateService _exchangeRateService;
    private readonly ILogger<FetchNbpDataJob> _logger;

    public FetchNbpDataJob(INbpExchangeRateService exchangeRateService, ILogger<FetchNbpDataJob> logger)
    {
        _exchangeRateService = exchangeRateService;
        _logger = logger;
    }

    public async Task Execute(string table)
    {
        _logger.LogInformation($"Start fetching NBP Exchange Rate Table {table}…");

        await _exchangeRateService.SyncExchangeRatesAsync(table);

        _logger.LogInformation($"End fetching NBP Exchange Rate Table {table}…");
    }
}