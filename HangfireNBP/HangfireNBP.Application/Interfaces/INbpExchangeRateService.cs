using Domain.Entities;

namespace HangfireNBP.Application.Interfaces;
public interface INbpExchangeRateService
{
    Task SyncExchangeRatesAsync(string table);
    Task<List<ExchangeRateTable>> GetAllTablesWithRatesAsync();
}