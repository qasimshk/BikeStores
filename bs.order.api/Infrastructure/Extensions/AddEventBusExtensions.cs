using bs.component.core.Extensions;
using bs.order.api.Infrastructure.Configurations;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace bs.order.api.Infrastructure.Extensions
{
    public static class AddEventBusExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services)
        {
            var appConfig = services.BuildServiceProvider().GetRequiredService<IOptions<ApplicationConfig>>().Value;

            services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);

            services.AddMassTransit(cfg =>
            {
                cfg.AddBusConfigurator(appConfig.EventBusConnection);
            });

            services.AddMassTransitHostedService();

            return services;
        }
    }
}
