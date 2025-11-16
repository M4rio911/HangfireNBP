using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HangfireNBP.Infrastructure.Extensions;

public static class HangfireExtensions
{
    public static IServiceCollection ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(cfg =>
        {
            cfg.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings();

            if (bool.Parse(configuration["Hangfire:UseInMemoryStorage"]))
            {
                cfg.UseInMemoryStorage();
            }
            else
            {
                cfg.UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection"));
            }
        });

        services.AddHangfireServer();

        return services;
    }
}