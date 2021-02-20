using bs.identity.domain.Models;
using MediatR;

namespace bs.identity.application.Commands.TokenAuthenticate
{
    public class TokenAuthenticateCommand : IRequest<UserLoginResponseDto>
    {
        public TokenAuthenticateCommand(UserLoginRequestDto requesDto)
        {
            EmailAddress = requesDto.Username.Trim();
            Password = requesDto.Password.Trim();
        }

        public string EmailAddress { get; private set; }

        public string Password { get; private set; }
    }
}
