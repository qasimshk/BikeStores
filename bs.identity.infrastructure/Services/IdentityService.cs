using bs.component.sharedkernal.Exceptions;
using bs.identity.domain.Abstractions;
using bs.identity.infrastructure.Configurations;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace bs.identity.infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IdentityServerConfiguration _identityServerConfiguration;

        public IdentityService(IHttpClientFactory httpClient, IOptions<IdentityServerConfiguration> config)
        {
            _httpClient = httpClient;
            _identityServerConfiguration = config.Value ?? throw new UnavailableConfigurationException(nameof(IdentityServerConfiguration));
        }

        public async Task<HttpResponseMessage> GenerateToken(string email, string password, CancellationToken cancellationToken)
        {
            using (var http = _httpClient.CreateClient("IdentityService"))
            {
                return await http.PostAsync("connect/token",
                    new FormUrlEncodedContent(SetGenerateTokenRequest(email, password)), cancellationToken);
            }
        }

        public async Task<HttpResponseMessage> RefreshToken(string refreshToken, CancellationToken cancellationToken)
        {
            using (var http = _httpClient.CreateClient("IdentityService"))
            {
                return await http.PostAsync("connect/token",
                    new FormUrlEncodedContent(SetRefreshTokenRequest(refreshToken)), cancellationToken);
            }
        }

        private List<KeyValuePair<string, string>> SetGenerateTokenRequest(string email, string password)
        {
            return new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("client_id", _identityServerConfiguration.ClientId),
                new KeyValuePair<string, string>("client_secret", _identityServerConfiguration.ClientSecret),
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", email),
                new KeyValuePair<string, string>("password", password),
            };
        }

        private List<KeyValuePair<string, string>> SetRefreshTokenRequest(string refreshToken)
        {
            return new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("client_id", _identityServerConfiguration.ClientId),
                new KeyValuePair<string, string>("client_secret", _identityServerConfiguration.ClientSecret),
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("refresh_token", refreshToken)
            };
        }
    }
}
