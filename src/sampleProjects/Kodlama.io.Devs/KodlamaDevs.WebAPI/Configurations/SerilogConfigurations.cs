using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace KodlamaDevs.WebAPI.Configurations
{
    // TODO: Remove this
    public static class SerilogConfigurations
    {
        public static Serilog.ILogger Logger { get; }

        static SerilogConfigurations()
        {
            Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Warning)
                .WriteTo.File(new JsonFormatter(), "logs/log.json", rollingInterval: RollingInterval.Day)
                .Enrich.FromLogContext()
                .CreateLogger();
        }
    }
}
