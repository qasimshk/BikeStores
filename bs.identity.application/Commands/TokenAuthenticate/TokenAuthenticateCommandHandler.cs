using System.Net;
using bs.identity.domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using bs.component.sharedkernal.Exceptions;

namespace bs.identity.application.Commands.TokenAuthenticate
{
    public class TokenAuthenticateCommandHandler : IRequestHandler<TokenAuthenticateCommand, UserLoginResponseDto>
    {
        public TokenAuthenticateCommandHandler()
        {
            
        }

        public async Task<UserLoginResponseDto> Handle(TokenAuthenticateCommand request, CancellationToken cancellationToken)
        {
            throw new AuthenticationFailedException("Invalid credentials");

            //return await Task.Run(() => new UserLoginResponseDto
            //{
            //    ExpireIn = 1234,
            //    StatusCode = HttpStatusCode.Accepted,
            //    Token = "testing token handler"
            //});
        }
    }
}
