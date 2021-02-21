using bs.identity.domain.Abstractions;
using bs.identity.infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace bs.identity.api.Infrastructure.Extensions
{
    public static class AddApplicationModulesExtensions
    {
        public static IServiceCollection AddApplicationModules(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IIdentityService, IdentityService>();


            return services;
        }
    }
}
