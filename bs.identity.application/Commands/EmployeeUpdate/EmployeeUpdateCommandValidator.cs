using FluentValidation;
using System;
using System.Linq;

namespace bs.identity.application.Commands.EmployeeUpdate
{
    public class EmployeeUpdateCommandValidator : AbstractValidator<EmployeeUpdateCommand>
    {
        public EmployeeUpdateCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .Length(2, 20).WithMessage("{PropertyName} length is invalid - {TotalLength}")
                .Must(MustBeValidName).WithMessage("{PropertyName} contains invalid characters");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .Length(2, 20).WithMessage("{PropertyName} length is invalid - {TotalLength}")
                .Must(MustBeValidName).WithMessage("{PropertyName} contains invalid characters");

            RuleFor(x => x.DateOfBirth)
                .Cascade(CascadeMode.Stop)
                .Must(MustBeValidAge).WithMessage("{PropertyName} is invalid");

            RuleFor(x => x.PhoneNumber)
                .Cascade(CascadeMode.Stop);
        }

        private static bool MustBeValidName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                name = name.Replace(" ", "");
                name = name.Replace("-", "");
                return name.All(char.IsLetter);
            }
            return true;
        }

        private static bool MustBeValidAge(DateTime? date)
        {
            if (date != null)
            {
                int currentYear = DateTime.Now.Year;
                int dobYear = (int)(date?.Year);

                return dobYear <= currentYear && dobYear > (currentYear - 120);
            }
            return true;
        }
    }
}
