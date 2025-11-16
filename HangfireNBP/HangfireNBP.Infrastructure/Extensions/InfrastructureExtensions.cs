using HangfireNBP.Application.Interfaces;
using HangfireNBP.Application.Parsing;
using HangfireNBP.Application.Services;
using HangfireNBP.Infrastructure.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace HangfireNBP.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DbConnection"))
        );

        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<AppDbContext>()!);
        
        services.AddScoped<INbpExchangeRateService, NbpExchangeRateService>();
        services.AddScoped<INbpXmlParser, NbpXmlParser>();

        services.AddHttpClient<INbpExchangeRateService, NbpExchangeRateService>(client =>
        {
            client.BaseAddress = new Uri(configuration["URLs:NBPApi"]);
        });

        return services;
    }
}