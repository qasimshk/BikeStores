using System.Collections.Generic;

namespace bs.gateway.api.Configuration
{
    public class IdentityServerConfig
    {
        public string IdentityServerURL { get; set; }
        public string AuthenticationProviderKey { get; set; }
        public IList<string> Microservices { get; set; }
    }
}
