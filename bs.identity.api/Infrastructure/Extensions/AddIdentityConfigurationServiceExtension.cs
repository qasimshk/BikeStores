using bs.identity.api.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace bs.identity.api.Infrastructure.Extensions
{
    public static class AddIdentityConfigurationServiceExtension
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration configuration)
        {
            // Settings
            var identitysettings = new IdentityServerConfiguration();
            configuration.Bind("IdentityServerConfiguration", identitysettings);

            // Get list of microservices
            List<string> microservices = configuration.GetSection("Microservices:APIs").Get<List<string>>();

            // Set identity service configuration
            services.Configure<IdentityServerConfiguration>(opt =>
            {
                opt.ClientId = identitysettings.ClientId;
                opt.ClientSecret = identitysettings.ClientSecret;
                opt.IdentityServerURL = identitysettings.IdentityServerURL;
            });

            // Identity service setting
            services.AddHttpClient("IdentityService", client =>
            {
                client.BaseAddress = new Uri(identitysettings.IdentityServerURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            // Configuration of Identity Server
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                .AddInMemoryIdentityResources(IdentityServerServiceConfiguration.GetIdentityResource())
                .AddInMemoryApiResources(IdentityServerServiceConfiguration.GetApiResources(microservices))
                .AddInMemoryClients(IdentityServerServiceConfiguration.GetClients(identitysettings, microservices));

            return services;
        }
    }
}
