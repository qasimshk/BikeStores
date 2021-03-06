using bs.component.core.Extensions;
using bs.inventory.api.Infrastructure.Extensions;
using bs.inventory.application.Extensions;
using bs.inventory.infrastructure.Persistence.Context;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace bs.inventory.api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private const string SwaggerJson = "/swagger/v1/swagger.json";
        private const string ServiceName = "Inventory Microservice";

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddServiceConfiguration(_configuration)
                .AddApplicationMvc(ServiceName)
                .AddApplicationDbContext<InventoryDbContext>(_configuration)
                .AddHandlers()
                .AddApplicationLogging(_configuration)
                .AddEventBus()
                .AddApplicationModules();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint(SwaggerJson, ServiceName));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.EnableDependencyInjection();
                endpoints.Select().Expand().Filter().Count().OrderBy();
                endpoints.MapControllers();
            });
        }
    }
}
