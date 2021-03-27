using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace bs.component.core.Extensions
{
    public static class ApplicationLoggingExtension
    {
        public static IServiceCollection AddApplicationLogging(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .CreateLogger();

            services.AddLogging(log => log.AddSerilog(dispose: true));
            return services;
        }
    }
}
