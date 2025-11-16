using Microsoft.Extensions.Hosting;
using Serilog;

namespace HangfireNBP.Infrastructure.Extensions;

public static class LogExtensions
{
    public static void ConfigureSerilog(this IHostBuilder host)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        host.UseSerilog();
    }
}