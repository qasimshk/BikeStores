using AutoMapper;
using bs.component.sharedkernal.Exceptions;
using bs.identity.domain.Entities;
using bs.identity.domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace bs.identity.infrastructure.Persistence.Queries.GetEmployeeInformation
{
    public class GetEmployeeInformationQueryHandler : IRequestHandler<GetEmployeeInformationQuery, EmployeeInformationDto>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;

        public GetEmployeeInformationQueryHandler(UserManager<Employee> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<EmployeeInformationDto> Handle(GetEmployeeInformationQuery request, CancellationToken cancellationToken)
        {
            var employee = await _userManager.FindByIdAsync(request.EmployeeId.ToString());

            if (employee == null)
            {
                throw new NotFoundException("Employee not found");
            }

            return _mapper.Map<EmployeeInformationDto>(employee);
        }
    }
}
