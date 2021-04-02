using bs.component.sharedkernal.Extensions;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace bs.order.api.Infrastructure.Extensions
{
    public static class AddEventBusExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, string eventBusConnection)
        {
            services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);

            services.AddMassTransit(cfg =>
            {
                cfg.ApplyCustomMassTransitConfiguration();

                cfg.SetKebabCaseEndpointNameFormatter();

                cfg.UsingRabbitMq((context, rabbitMqBusFactoryConfigurator) =>
                {
                    rabbitMqBusFactoryConfigurator.Host(eventBusConnection);

                    MessageDataDefaults.ExtraTimeToLive = TimeSpan.FromDays(1);
                    MessageDataDefaults.Threshold = 2000;
                    MessageDataDefaults.AlwaysWriteToRepository = false;

                    rabbitMqBusFactoryConfigurator.ConfigureEndpoints(context);
                });
            });

            services.AddMassTransitHostedService();

            return services;
        }
    }
}
