using FluentValidation;

namespace bs.identity.application.Commands.TokenAuthenticate
{
    public class TokenAuthenticateCommandValidator : AbstractValidator<TokenAuthenticateCommand>
    {
        public TokenAuthenticateCommandValidator()
        {
            RuleFor(x => x.EmailAddress)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Please provide email address")
                .MaximumLength(500).NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Please provide password")
                .MaximumLength(500).NotEmpty();
        }
    }
}
