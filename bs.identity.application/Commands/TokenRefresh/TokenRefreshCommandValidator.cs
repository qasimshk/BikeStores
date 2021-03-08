using FluentValidation;

namespace bs.identity.application.Commands.TokenRefresh
{
    public class TokenRefreshCommandValidator : AbstractValidator<TokenRefreshCommand>
    {
        public TokenRefreshCommandValidator()
        {
            RuleFor(x => x.RefreshToken)
                .Cascade(cascadeMode: CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide refresh token")
                .MaximumLength(65).WithMessage("invalid character count");
        }
    }
}
