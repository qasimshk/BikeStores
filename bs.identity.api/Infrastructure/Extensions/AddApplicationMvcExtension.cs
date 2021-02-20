using bs.component.sharedkernal.Filters;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace bs.identity.api.Infrastructure.Extensions
{
    public static class AddApplicationMvcExtension
    {
        private const string ServiceVersion = "v1";
        private const string ServiceName = "Identity Microservice";

        public static IServiceCollection AddApplicationMvc(this IServiceCollection services)
        {

            services.AddControllers(options =>
                    options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(ServiceVersion, new OpenApiInfo
                {
                    Title = ServiceName,
                    Version = ServiceVersion
                });
            });

            return services;
        }
    }
}
