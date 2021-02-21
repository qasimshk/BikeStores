using bs.identity.api.Infrastructure.Configuration;
using bs.identity.infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace bs.identity.api.Infrastructure.Extensions
{
    public static class AddIdentityConfigurationServiceExtension
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration configuration)
        {
            // Settings
            var identitySettings = configuration.GetSection(nameof(IdentityServerConfiguration)).Get<IdentityServerConfiguration>();
            
            // Set identity service configuration
            services.Configure<IdentityServerConfiguration>(opt =>
            {
                opt.ClientId = identitySettings.ClientId;
                opt.ClientSecret = identitySettings.ClientSecret;
                opt.IdentityServerURL = identitySettings.IdentityServerURL;
            });

            // Identity service setting
            services.AddHttpClient("IdentityService", client =>
            {
                client.BaseAddress = new Uri(identitySettings.IdentityServerURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            // Configuration of Identity Server
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                .AddInMemoryIdentityResources(IdentityServerServiceConfiguration.GetIdentityResource())
                .AddInMemoryApiResources(IdentityServerServiceConfiguration.GetApiResources(identitySettings.Microservices))
                .AddInMemoryClients(IdentityServerServiceConfiguration.GetClients(identitySettings, identitySettings.Microservices))
                .AddProfileService<ProfileService>();

            return services;
        }
    }
}
