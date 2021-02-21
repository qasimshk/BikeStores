using bs.identity.domain.Abstractions;
using bs.identity.domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;

namespace bs.identity.application.Commands.TokenRefresh
{
    public class TokenRefreshCommandHandler : IRequestHandler<TokenRefreshCommand, UserLoginResponseDto>
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<TokenRefreshCommandHandler> _logger;

        public TokenRefreshCommandHandler(IIdentityService identityService, ILogger<TokenRefreshCommandHandler> logger)
        {
            _identityService = identityService;
            _logger = logger;
        }

        public async Task<UserLoginResponseDto> Handle(TokenRefreshCommand request, CancellationToken cancellationToken)
        {
            // Calling identity service
            var identityServiceResponse = await _identityService.RefreshToken(request.RefreshToken, cancellationToken);

            if (!identityServiceResponse.IsSuccessStatusCode)
            {
                throw new AuthenticationException("Invalid token");
            }

            var serviceResponse = await identityServiceResponse.Content.ReadAsStringAsync();

            return new UserLoginResponseDto
            {
                AccessToken = JObject.Parse(serviceResponse)["access_token"].Value<string>(),
                RefreshToken = JObject.Parse(serviceResponse)["refresh_token"].Value<string>(),
                ExpireIn = JObject.Parse(serviceResponse)["expires_in"].Value<int>(),
                StatusCode = HttpStatusCode.Accepted
            };
        }
    }
}
