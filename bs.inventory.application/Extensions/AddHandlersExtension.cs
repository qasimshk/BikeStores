using bs.component.sharedkernal.Behaviours;
using bs.inventory.infrastructure.Persistence.Queries.GetBasketValidated;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace bs.inventory.application.Extensions
{
    public static class AddHandlersExtension
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(GetBasketValidatedQueryHandler).GetTypeInfo().Assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
