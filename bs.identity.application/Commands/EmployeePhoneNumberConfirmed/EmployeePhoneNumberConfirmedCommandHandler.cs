using System.Linq;
using bs.component.sharedkernal.Exceptions;
using bs.identity.domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace bs.identity.application.Commands.EmployeePhoneNumberConfirmed
{
    public class EmployeePhoneNumberConfirmedCommandHandler : IRequestHandler<EmployeePhoneNumberConfirmedCommand>
    {
        private readonly UserManager<Employee> _userManager;

        public EmployeePhoneNumberConfirmedCommandHandler(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(EmployeePhoneNumberConfirmedCommand request, CancellationToken cancellationToken)
        {
            var employee = await _userManager.FindByIdAsync(request.EmployeeId.ToString());

            if (employee == null)
            {
                throw new NotFoundException("Employee not found");
            }

            if (!await _userManager.IsPhoneNumberConfirmedAsync(employee))
            {
                employee.PhoneNumberConfirmed = true;

                var result = await _userManager.UpdateAsync(employee);

                if (!result.Succeeded)
                {
                    throw new ValidationException(result.Errors.Select(error => new ValidationFailure("", error.Description)));
                }

                return Unit.Value;
            }

            throw new BadRequestException("Employee's phone number is already confirmed");
        }
    }
}
