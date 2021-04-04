using bs.order.domain.Repositories;
using bs.order.infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace bs.order.service.Infrastructure.Extensions
{
    public static class AddApplicationModulesExtensions
    {
        public static IServiceCollection AddApplicationModules(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IPaymentRepository, PaymentRepository>();

            return services;
        }
    }
}
