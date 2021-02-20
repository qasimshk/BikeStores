using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace bs.identity.api.Infrastructure.Extensions
{
    public static class AddApplicationLoggingExtension
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
