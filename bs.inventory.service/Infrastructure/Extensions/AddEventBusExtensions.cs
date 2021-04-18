using bs.component.core.Extensions;
using bs.inventory.domain.Entities;
using bs.inventory.infrastructure.Persistence.Context;
using bs.inventory.service.Consumers;
using bs.inventory.service.Infrastructure.Configurations;
using bs.inventory.service.Ochestrator;
using MassTransit;
using MassTransit.Definition;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace bs.inventory.service.Infrastructure.Extensions
{
    public static class AddEventBusExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var appConfig = services.BuildServiceProvider().GetRequiredService<IOptions<ApplicationConfig>>().Value;

            services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);

            services.AddMassTransit(cfg =>
            {
                cfg.AddRabbitMqMessageScheduler();

                cfg.AddSagaStateMachine<InventoryStateMachine, InventoryStatus>()
                    .EntityFrameworkRepository(ef =>
                    {
                        ef.ConcurrencyMode = ConcurrencyMode.Pessimistic;
                        ef.AddDbContext<DbContext, InventoryDbContext>((provider, builder) =>
                        {
                            builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
                            {
                                sqlOptions.MigrationsHistoryTable($"__{nameof(InventoryDbContext)}");
                            });
                        });
                    });

                cfg.AddConsumersFromNamespaceContaining<CreateBasketConsumer>();

                cfg.AddBusConfigurator(appConfig.EventBusConnection);
            });

            return services;
        }
    }
}
