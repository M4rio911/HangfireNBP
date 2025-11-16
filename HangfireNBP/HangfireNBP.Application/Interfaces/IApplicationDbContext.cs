using Domain.Entities;
using HangfireNBP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HangfireNBP.Application.Interfaces;
public interface IApplicationDbContext
{
    DbSet<ExchangeRate> ExchangeRates { get; set; }
    DbSet<ExchangeRateTable> ExchangeRateTables { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
