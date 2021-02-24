using bs.identity.domain.Models;
using MediatR;
using System;

namespace bs.identity.infrastructure.Persistence.Queries.GetEmployeeInformation
{
    public class GetEmployeeInformationQuery : IRequest<EmployeeInformationDto>
    {
        public GetEmployeeInformationQuery(Guid employeeId)
        {
            EmployeeId = employeeId;
        }

        public Guid EmployeeId { get; private set; }
    }
}
