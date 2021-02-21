using bs.identity.application.Commands.TokenAuthenticate;
using FluentValidation;

namespace bs.identity.application.Commands.TokenRefresh
{
    public class TokenRefreshCommandValidator : AbstractValidator<TokenAuthenticateCommand>
    {
        public TokenRefreshCommandValidator()
        {
            RuleFor(x => x.EmailAddress)
                .Cascade(cascadeMode: CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide refresh token")
                .MaximumLength(65).WithMessage("invalid character count");
        }
    }
}
