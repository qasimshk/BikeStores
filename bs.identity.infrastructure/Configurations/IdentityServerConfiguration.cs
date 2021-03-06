using System.Collections.Generic;

namespace bs.identity.infrastructure.Configurations
{
    public class IdentityServerConfiguration
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string IdentityServerURL { get; set; }
        public IList<string> Microservices { get; set; }
    }
}
