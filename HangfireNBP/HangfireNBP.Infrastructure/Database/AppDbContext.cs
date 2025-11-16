using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using HangfireNBP.Application.Interfaces;
using HangfireNBP.Domain.Entities;

namespace HangfireNBP.Infrastructure.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<ExchangeRate> ExchangeRates { get; set; }
    public DbSet<ExchangeRateTable> ExchangeRateTables { get; set; }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}