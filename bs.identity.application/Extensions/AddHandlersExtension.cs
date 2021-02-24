using bs.component.sharedkernal.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using bs.identity.infrastructure.Persistence.Queries.GetEmployeeInformation;

namespace bs.identity.application.Extensions
{
    public static class AddHandlersExtension
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            
            services.AddAutoMapper(typeof(GetEmployeeInformationQueryMapper).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetEmployeeInformationQueryHandler).GetTypeInfo().Assembly);
            
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
