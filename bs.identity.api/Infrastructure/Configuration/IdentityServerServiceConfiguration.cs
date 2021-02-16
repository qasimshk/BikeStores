using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace bs.identity.api.Infrastructure.Configuration
{
    internal static class IdentityServerServiceConfiguration
    {
        internal static IEnumerable<IdentityResource> GetIdentityResource()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        internal static IEnumerable<ApiResource> GetApiResources(IList<string> microservices)
        {
            var services = new List<ApiResource>();

            foreach (string service in microservices)
            {
                services.Add(new ApiResource(service, service));
            }

            return services;
        }

        internal static IEnumerable<Client> GetClients(IdentityServerConfiguration identityServerConfig, IList<string> microservices)
        {
            microservices.Add(IdentityServerConstants.StandardScopes.OpenId);
            microservices.Add(IdentityServerConstants.StandardScopes.Profile);
            microservices.Add(IdentityServerConstants.StandardScopes.OfflineAccess);
            
            return new List<Client>()
            {
                new Client
                {
                    ClientName = "Microservices",
                    ClientId = identityServerConfig.ClientId,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = microservices,
                    ClientSecrets =
                    {
                        new Secret(identityServerConfig.ClientSecret.Sha256())
                    },
                    AllowOfflineAccess = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 900, //Seconds,
                    //AlwaysIncludeUserClaimsInIdToken = true, // Put all the claims in the id token
                    RequireConsent = false
                }
            };
        }
    }
}
