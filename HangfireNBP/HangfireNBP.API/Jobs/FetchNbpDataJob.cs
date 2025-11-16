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

    public async Task Execute()
    {
        _logger.LogInformation("Fetching NBP Exchange Rate Table B…");

        await _exchangeRateService.SyncExchangeRatesAsync("b");

        _logger.LogInformation("End fetching NBP Exchange Rate Table B…");
    }
}