using bs.component.sharedkernal.Exceptions;
using bs.identity.domain.Entities;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace bs.identity.application.Commands.EmployeeUpdate
{
    public class EmployeeUpdateCommandHandler : IRequestHandler<EmployeeUpdateCommand>
    {
        private readonly UserManager<Employee> _userManager;
        private readonly ILogger<EmployeeUpdateCommandHandler> _logger;

        public EmployeeUpdateCommandHandler(UserManager<Employee> userManager, ILogger<EmployeeUpdateCommandHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<Unit> Handle(EmployeeUpdateCommand request, CancellationToken cancellationToken)
        {
            var employee = await _userManager.FindByIdAsync(request.EmployeeId.ToString());

            if (employee is null)
            {
                throw new NotFoundException("Invalid employee");
            }

            if (request.PhoneNumber != null && request.PhoneNumber != employee.PhoneNumber)
            {
                employee.PhoneNumber = request.PhoneNumber.Trim();
            }

            if (!string.IsNullOrEmpty(request.FirstName) && request.FirstName != employee.FirstName)
            {
                employee.FirstName = request.FirstName.Trim();
            }

            if (!string.IsNullOrEmpty(request.LastName) && request.LastName != employee.LastName)
            {
                employee.LastName = request.LastName.Trim();
            }

            if (request.DateOfBirth != null && request.DateOfBirth != employee.DateOfBirth)
            {
                employee.DateOfBirth = (DateTime)request.DateOfBirth;
            }

            var response = await _userManager.UpdateAsync(employee);

            if (!response.Succeeded)
            {
                LogErrors(response.Errors);
                throw new ValidationException(response.Errors.Select(error => new ValidationFailure("", error.Description)));
            }

            return Unit.Value;
        }

        private void LogErrors(IEnumerable<IdentityError> errors)
        {
            foreach (var error in errors)
            {
                _logger.LogError($"Code: {error.Code} Description: {error.Description}");
            }
        }
    }
}
