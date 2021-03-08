using bs.identity.domain.Models;
using MediatR;
using System;

namespace bs.identity.application.Commands.EmployeeUpdate
{
    public class EmployeeUpdateCommand : IRequest
    {
        protected EmployeeUpdateCommand() { }

        public EmployeeUpdateCommand(Guid employeeId, EmployeeUpdateRequestDto request)
        {
            EmployeeId = employeeId;
            FirstName = request.FirstName;
            LastName = request.LastName;
            DateOfBirth = request.DateOfBirth;
            PhoneNumber = request.PhoneNumber;
        }

        public Guid EmployeeId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime? DateOfBirth { get; private set; }
        public string PhoneNumber { get; private set; }
    }
}
