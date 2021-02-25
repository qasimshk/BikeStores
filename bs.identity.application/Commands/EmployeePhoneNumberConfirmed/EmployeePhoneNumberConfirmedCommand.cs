using MediatR;
using System;

namespace bs.identity.application.Commands.EmployeePhoneNumberConfirmed
{
    public class EmployeePhoneNumberConfirmedCommand : IRequest<Unit>
    {
        public EmployeePhoneNumberConfirmedCommand(Guid employeeId)
        {
            EmployeeId = employeeId;
        }

        public Guid EmployeeId { get; private set; }
    }
}
