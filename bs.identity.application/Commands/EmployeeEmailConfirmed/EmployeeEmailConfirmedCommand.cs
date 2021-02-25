using MediatR;
using System;

namespace bs.identity.application.Commands.EmployeeEmailConfirmed
{
    public class EmployeeEmailConfirmedCommand : IRequest<Unit>
    {
        public EmployeeEmailConfirmedCommand(Guid employeeId, string token)
        {
            EmployeeId = employeeId;
            Token = token.Trim();
        }

        public Guid EmployeeId { get; private set; }
        public string Token { get; private set; }
    }
}
