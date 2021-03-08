using bs.identity.domain.Models;
using bs.identity.infrastructure.Persistence.Filters;
using MediatR;
using System.Collections.Generic;

namespace bs.identity.infrastructure.Persistence.Queries.SearchEmployees
{
    public class SearchEmployeesQuery : IRequest<List<EmployeeInformationDto>>
    {
        public SearchEmployeesQuery(EmployeeFilter filter)
        {
            FirstName = filter.FirstName;
            LastName = filter.LastName;
            Email = filter.Email;
            PhoneNumber = filter.PhoneNumber;
            StoreId = filter.StoreId;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public int? StoreId { get; private set; }
        public string PhoneNumber { get; private set; }
    }
}
