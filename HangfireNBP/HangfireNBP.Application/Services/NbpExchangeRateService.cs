using Domain.Entities;
using HangfireNBP.Application.DTO;
using HangfireNBP.Application.Interfaces;
using HangfireNBP.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace HangfireNBP.Application.Services;

public class NbpExchangeRateService : INbpExchangeRateService
{
    private readonly ILogger<NbpExchangeRateService> _logger;
    private readonly IApplicationDbContext _dbContext;
    private readonly INbpXmlParser _xmlParser;
    private readonly HttpClient _nbpClient;

    public NbpExchangeRateService(ILogger<NbpExchangeRateService> logger, IApplicationDbContext dbContext, INbpXmlParser xmlParser, HttpClient nbpClient)
    {
        _logger = logger;
        _dbContext = dbContext;
        _xmlParser = xmlParser;
        _nbpClient = nbpClient;
    }

    public async Task SyncExchangeRatesAsync(string tableCode)
    {
        _logger.LogInformation($"Fetching NBP Exchange Rate Table {tableCode}…");

        var xmlContent = await _nbpClient.GetStringAsync($"exchangerates/tables/{tableCode}/?format=xml");
        var parsedTable = _xmlParser.Parse(xmlContent);

        var dbTable = await GetOrCreateExchangeRateTableAsync(parsedTable);

        ApplyRates(parsedTable.Rates, dbTable);

        await _dbContext.SaveChangesAsync();

        _logger.LogInformation($"Saved Exchange Rate Table: {dbTable.FileNumber} with {parsedTable.Rates.Count} rates.");
    }

    private async Task<ExchangeRateTable> GetOrCreateExchangeRateTableAsync(ParsedExchangeRateTable parsed)
    {
        var dbTable = await _dbContext.ExchangeRateTables
            .Include(x => x.ExchangeRates)
            .FirstOrDefaultAsync(x => x.TableCode == parsed.TableCode);
        
        if (dbTable == null)
        {
            dbTable = new ExchangeRateTable
            {
                TableCode = parsed.TableCode,
                FileNumber = parsed.FileNumber,
                EffectiveDate = parsed.EffectiveDate,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
                ExchangeRates = new List<ExchangeRate>()
            };

            await _dbContext.ExchangeRateTables.AddAsync(dbTable);
        }
        else
        {
            dbTable.FileNumber = parsed.FileNumber;
            dbTable.EffectiveDate = parsed.EffectiveDate;
            dbTable.Modified = DateTime.UtcNow;
        }

        return dbTable;
    }

    private static void ApplyRates(List<ParsedExchangeRate> parsedRates, ExchangeRateTable dbTable)
    {
        foreach (var rate in parsedRates)
        {
            var existing = dbTable.ExchangeRates
                .FirstOrDefault(x => x.Code == rate.Code);

            if (existing == null)
            {
                dbTable.ExchangeRates.Add(new ExchangeRate
                {
                    Name = rate.Name,
                    Code = rate.Code,
                    Mid = rate.Mid,
                    ExchangeRateTable = dbTable
                });
            }
            else
            {
                existing.Name = rate.Name;
                existing.Mid = rate.Mid;
                existing.Modified = DateTime.UtcNow;
            }
        }
    }

    public async Task<List<ExchangeRateTable>> GetAllTablesWithRatesAsync()
    {
        return await _dbContext.ExchangeRateTables
            .Include(t => t.ExchangeRates)
            .OrderByDescending(t => t.EffectiveDate)
            .AsNoTracking()
            .ToListAsync();
    }
}