using bs.inventory.domain.Respositories;
using bs.inventory.infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace bs.inventory.service.Infrastructure.Extensions
{
    public static class AddApplicationModulesExtensions
    {
        public static IServiceCollection AddApplicationModules(this IServiceCollection services)
        {
            services.AddScoped<IBasketRepository, BasketRepository>();
            
            services.AddScoped<IBrandRepository, BrandRepository>();
            
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            
            services.AddScoped<IStoreRepository, StoreRepository>();
            
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
