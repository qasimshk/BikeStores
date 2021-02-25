using MediatR;
using System;

namespace bs.identity.application.Commands.EmployeeEmailConfirmedToken
{
    public class EmployeeEmailConfirmedTokenCommand : IRequest<string>
    {
        public EmployeeEmailConfirmedTokenCommand(Guid employeeId)
        {
            EmployeeId = employeeId;
        }

        public Guid EmployeeId { get; private set; }
    }
}
