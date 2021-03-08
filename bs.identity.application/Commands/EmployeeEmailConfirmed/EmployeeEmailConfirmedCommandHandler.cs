using System.Linq;
using bs.component.sharedkernal.Exceptions;
using bs.identity.domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace bs.identity.application.Commands.EmployeeEmailConfirmed
{
    public class EmployeeEmailConfirmedCommandHandler : IRequestHandler<EmployeeEmailConfirmedCommand>
    {
        private readonly UserManager<Employee> _userManager;

        public EmployeeEmailConfirmedCommandHandler(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(EmployeeEmailConfirmedCommand request, CancellationToken cancellationToken)
        {
            var employee = await _userManager.FindByIdAsync(request.EmployeeId.ToString());

            if (employee == null)
            {
                throw new NotFoundException("Employee not found");
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(employee);

            if (!await _userManager.IsEmailConfirmedAsync(employee))
            {
                var result = await _userManager.ConfirmEmailAsync(employee, token);

                if (!result.Succeeded)
                {
                    throw new ValidationException(result.Errors.Select(error => new ValidationFailure("", error.Description)));
                }

                return Unit.Value;
            }

            throw new BadRequestException("Employee's email address is already confirmed");
        }
    }
}
