using bs.identity.domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace bs.identity.application.Commands.EmployeeRegistration
{
    public class EmployeeRegistrationCommandValidator : AbstractValidator<EmployeeRegistrationCommand>
    {
        private readonly UserManager<Employee> _userManager;

        public EmployeeRegistrationCommandValidator(UserManager<Employee> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide your {PropertyName}")
                .Length(2, 20).WithMessage("{PropertyName} length is invalid - {TotalLength}")
                .Must(MustBeValidName).WithMessage("{PropertyName} contains invalid characters");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide your {PropertyName}")
                .Length(2, 20).WithMessage("{PropertyName} length is invalid - {TotalLength}")
                .Must(MustBeValidName).WithMessage("{PropertyName} contains invalid characters");

            RuleFor(x => x.DateOfBirth)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide valid date of birth")
                .Must(MustBeValidAge).WithMessage("{PropertyName} is invalid");

            RuleFor(x => x.EmailAddress)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide email address")
                .MaximumLength(500).NotEmpty()
                .MustAsync(MustBeUniqueEmail).WithMessage("The specified email address already exists")
                .EmailAddress();

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide password")
                .MaximumLength(500).NotEmpty();

            RuleFor(x => x.Designation)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide employee role")
                .InclusiveBetween(1,3).WithMessage("Invalid role Id");

            RuleFor(x => x.StoreId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide store Id");

            RuleFor(x => x.PhoneNumber)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide a valid contact number");
        }
        
        private async Task<bool> MustBeUniqueEmail(string emailAddress, CancellationToken cancellationToken)
        {
            var employee = await _userManager.FindByEmailAsync(emailAddress);
            
            return employee == null;
        }

        private static bool MustBeValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");
            return name.All(char.IsLetter);
        }

        private static bool MustBeValidAge(DateTime date)
        {
            int currentYear = DateTime.Now.Year;
            int dobYear = date.Year;

            return dobYear <= currentYear && dobYear > (currentYear - 120);
        }
    }
}
