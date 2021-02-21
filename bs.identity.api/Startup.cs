using bs.identity.api.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using bs.identity.application.Extensions;

namespace bs.identity.api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private const string _swaggerJson = "/swagger/v1/swagger.json";
        private const string _serviceName = "Identity Microservice";

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext(_configuration)
                .AddApplicationMvc()
                .AddHandlers()
                .AddApplicationLogging(_configuration)
                .AddIdentityService(_configuration)
                .AddApplicationModules(_configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint(_swaggerJson, _serviceName));
            }

            app.UseRouting();
            app.UseIdentityServer();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
