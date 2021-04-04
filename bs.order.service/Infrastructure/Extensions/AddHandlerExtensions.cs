using bs.component.sharedkernal.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace bs.order.service.Infrastructure.Extensions
{
    public static class AddHandlerExtensions
    {
        public static IServiceCollection AddHandler(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            return services;
        }
    }
}
