using AutoMapper;
using bs.component.sharedkernal.Exceptions;
using bs.identity.domain.Entities;
using bs.identity.domain.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using bs.component.sharedkernal.Extensions;

namespace bs.identity.infrastructure.Persistence.Queries.SearchEmployees
{
    public class SearchEmployeesQueryHandler : IRequestHandler<SearchEmployeesQuery, List<EmployeeInformationDto>>
    {
        private readonly IDbConnection _db;
        private readonly IMapper _mapper;
        
        public SearchEmployeesQueryHandler(IDbConnection db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<EmployeeInformationDto>> Handle(SearchEmployeesQuery request, CancellationToken cancellationToken)
        {
            if (!request.AreAllPropertiesNull())
            {
                throw new BadRequestException("No search parameters provided");
            }

            var builder = new SqlBuilder();

            var query = builder.AddTemplate(
                "SELECT [Id],[FirstName],[LastName],[Email],[EmailConfirmed],[PhoneNumber],[PhoneNumberConfirmed],[Designation],[DateOfBirth] FROM [AspNetUsers] /**where**/");

            if (!string.IsNullOrEmpty(request.FirstName))
                builder.Where("FirstName = @FirstName", new {request.FirstName});

            if (!string.IsNullOrEmpty(request.LastName))
                builder.Where("LastName = @LastName", new { request.LastName });

            if (!string.IsNullOrEmpty(request.Email))
                builder.Where("Email = @Email", new { request.Email });

            if (!string.IsNullOrEmpty(request.PhoneNumber))
                builder.Where("PhoneNumber = @PhoneNumber", new { request.PhoneNumber });

            if (!(request.StoreId is null) && request.StoreId > 0)
                builder.Where("StoreId = @StoreId", new { request.StoreId });
            
            var employees = await _db.QueryAsync<Employee>(query.RawSql, query.Parameters);
            
            if (!employees.Any())
            {
                throw new NotFoundException("Record not found");
            }

            return _mapper.Map<List<EmployeeInformationDto>>(employees);
        }
    }
}
