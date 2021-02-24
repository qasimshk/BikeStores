using AutoMapper;
using bs.identity.domain.Entities;
using bs.identity.domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using bs.component.sharedkernal.Exceptions;
using FluentValidation.Results;

namespace bs.identity.application.Commands.EmployeeRegistration
{
    public class EmployeeRegistrationCommandHandler : IRequestHandler<EmployeeRegistrationCommand>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;
        private readonly ILogger<EmployeeRegistrationCommandHandler> _logger;

        public EmployeeRegistrationCommandHandler(IMapper mapper, UserManager<Employee> userManager, ILogger<EmployeeRegistrationCommandHandler> logger)
        {
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<Unit> Handle(EmployeeRegistrationCommand request, CancellationToken cancellationToken)
        {
            var employee = _mapper.Map<Employee>(request);
            
            // Create Employee
            var userResult = await _userManager.CreateAsync(employee, request.Password);

            if (!userResult.Succeeded)
            {
                LogErrors(userResult.Errors, _logger);

                throw new ValidationException(userResult.Errors.Select(error => new ValidationFailure("", error.Description)));
            }
            
            // Add Claims
            var claimResult = await _userManager.AddClaimsAsync(employee, new List<Claim>
            {
                new Claim("given_name", request.FirstName),
                new Claim("family_name", request.LastName),
                new Claim("role", Enum.GetName(typeof(EmployeeRoles), request.Designation))
            });

            if (!claimResult.Succeeded)
            {
                LogErrors(claimResult.Errors, _logger);

                throw new ValidationException(claimResult.Errors.Select(error => new ValidationFailure("", error.Description)));
            }

            return Unit.Value;
        }

        private void LogErrors(IEnumerable<IdentityError> errors, ILogger logger)
        {
            foreach (var error in errors)
            {
                _logger.LogError($"Code: {error.Code} Description: {error.Description}");
            }
        }
    }
}
