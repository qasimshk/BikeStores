using bs.component.core.Extensions;
using bs.order.domain.Entities;
using bs.order.infrastructure.Persistence.Context;
using bs.order.service.Consumers;
using bs.order.service.Infrastructure.Configurations;
using bs.order.service.Workflow;
using MassTransit;
using MassTransit.Definition;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;

namespace bs.order.service.Infrastructure.Extensions
{
    public static class AddEventBusExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var appConfig = services.BuildServiceProvider().GetRequiredService<IOptions<ApplicationConfig>>().Value;

            services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);

            services.AddMassTransit(cfg =>
            {
                cfg.AddSagaStateMachine<OrderStateMachine, OrderState>()
                    .EntityFrameworkRepository(ef =>
                    {
                        ef.ConcurrencyMode = ConcurrencyMode.Pessimistic;
                        ef.AddDbContext<DbContext, OrderDbContext>((provider, builder) =>
                        {
                            builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
                            {
                                sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                                sqlOptions.MigrationsHistoryTable($"__{nameof(OrderDbContext)}");
                            });
                        });
                    });

                cfg.AddBusConfigurator(appConfig.EventBusConnection);

                cfg.AddRabbitMqMessageScheduler();

                cfg.AddConsumersFromNamespaceContaining<CustomerConsumer>();
            });

            services.AddMassTransitHostedService();

            return services;
        }

        
    }
}
