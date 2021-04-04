using bs.order.api.Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace bs.order.api.Infrastructure.Extensions
{
    public static class AddApplicationSettingsExtensions
    {   
        public static IServiceCollection AddServiceConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApplicationConfig>(configuration.GetSection(nameof(ApplicationConfig)));

            return services;
        }
    }
}
