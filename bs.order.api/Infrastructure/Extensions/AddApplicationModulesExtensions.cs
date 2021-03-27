using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace bs.order.api.Infrastructure.Extensions
{
    public static class AddApplicationModulesExtensions
    {
        public static IServiceCollection AddApplicationModules(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
