using bs.identity.domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using bs.component.sharedkernal.Exceptions;

namespace bs.identity.application.Commands.EmployeeEmailConfirmedToken
{
    public class EmployeePhoneNumberConfirmedTokenCommandHandler : IRequestHandler<EmployeeEmailConfirmedTokenCommand, string>
    {
        private readonly UserManager<Employee> _userManager;

        public EmployeePhoneNumberConfirmedTokenCommandHandler(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> Handle(EmployeeEmailConfirmedTokenCommand request, CancellationToken cancellationToken)
        {
            var employee = await _userManager.FindByIdAsync(request.EmployeeId.ToString());

            if (employee == null)
            {
                throw new NotFoundException("Employee not found");
            }

            return await _userManager.GenerateEmailConfirmationTokenAsync(employee);
        }
    }
}
