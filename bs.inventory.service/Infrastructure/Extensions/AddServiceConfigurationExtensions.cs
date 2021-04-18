using bs.inventory.service.Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace bs.inventory.service.Infrastructure.Extensions
{
    public static class AddServiceConfigurationExtensions
    {
        public static IServiceCollection AddServiceConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApplicationConfig>(configuration.GetSection(nameof(ApplicationConfig)));

            return services;
        }
    }
}
