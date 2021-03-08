using MediatR;
using System;

namespace bs.identity.application.Commands.EmployeeEmailConfirmed
{
    public class EmployeeEmailConfirmedCommand : IRequest<Unit>
    {
        public EmployeeEmailConfirmedCommand(Guid employeeId)
        {
            EmployeeId = employeeId;
        }

        public Guid EmployeeId { get; private set; }
    }
}
