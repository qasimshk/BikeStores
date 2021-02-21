using bs.identity.domain.Models;
using MediatR;

namespace bs.identity.application.Commands.TokenRefresh
{
    public class TokenRefreshCommand : IRequest<UserLoginResponseDto>
    {
        public TokenRefreshCommand(string refreshToken)
        {
            RefreshToken = refreshToken.Trim();
        }

        public string RefreshToken { get; private set; }
    }
}
