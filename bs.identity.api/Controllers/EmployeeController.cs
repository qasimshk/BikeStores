using bs.component.sharedkernal.Common;
using bs.identity.application.Commands.EmployeeEmailConfirmed;
using bs.identity.application.Commands.EmployeePhoneNumberConfirmed;
using bs.identity.application.Commands.EmployeeRegistration;
using bs.identity.domain.Models;
using bs.identity.infrastructure.Persistence.Filters;
using bs.identity.infrastructure.Persistence.Queries.GetEmployeeInformation;
using bs.identity.infrastructure.Persistence.Queries.SearchEmployees;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using bs.identity.application.Commands.EmployeeUpdate;

namespace bs.identity.api.Controllers
{
    public class EmployeeController : BaseController
    {
        [HttpGet("{employeeId}")]
        [EnableQuery()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Employee(Guid employeeId)
        {
            if (employeeId == Guid.Empty)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(new GetEmployeeInformationQuery(employeeId)));
        }

        [HttpGet("Search")]
        [EnableQuery()]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> EmployeeSearch([FromQuery] EmployeeFilter filter)
        {
            return Ok(await _mediator.Send(new SearchEmployeesQuery(filter)));
        }
        
        [HttpPost("Register")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadGateway)]
        public async Task<IActionResult> Register([FromBody] EmployeeRegisterRequestDto request)
        {
            return Ok(await _mediator.Send(_mapper.Map<EmployeeRegistrationCommand>(request)));
        }

        [HttpPut("{employeeId}/Update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadGateway)]
        public async Task<IActionResult> EmployeeUpdate([FromRoute] Guid employeeId, [FromBody] EmployeeUpdateRequestDto request)
        {
            if (employeeId == Guid.Empty)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(new EmployeeUpdateCommand(employeeId,request)));
        }

        [HttpPatch("{employeeId}/PhoneNumberConfirmed")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> PhoneNumberConfirmed(Guid employeeId)
        {
            if (employeeId == Guid.Empty)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(new EmployeePhoneNumberConfirmedCommand(employeeId)));
        }

        [HttpPatch("{employeeId}/EmailConfirmed")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EmailConfirmed(Guid employeeId)
        {
            if (employeeId == Guid.Empty)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(new EmployeeEmailConfirmedCommand(employeeId)));
        }
    }
}