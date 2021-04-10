using bs.component.core.Extensions;
using bs.inventory.api.Infrastructure.Extensions;
using bs.inventory.infrastructure.Persistence.Context;
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
                .AddApplicationMvc(ServiceName)
                .AddApplicationDbContext<InventoryDbContext>(_configuration)
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
                endpoints.MapControllers();
            });
        }
    }
}