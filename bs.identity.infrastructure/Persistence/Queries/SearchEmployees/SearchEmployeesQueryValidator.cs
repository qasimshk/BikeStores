using FluentValidation;

namespace bs.identity.infrastructure.Persistence.Queries.SearchEmployees
{
    public class SearchEmployeesQueryValidator : AbstractValidator<SearchEmployeesQuery>
    {
        public SearchEmployeesQueryValidator()
        {
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .Length(2, 20).WithMessage("{PropertyName} length is invalid - {TotalLength}");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .Length(2, 20).WithMessage("{PropertyName} length is invalid - {TotalLength}");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .EmailAddress();

            RuleFor(x => x.PhoneNumber)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(10);
        }
    }
}
