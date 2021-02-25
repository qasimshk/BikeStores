using bs.component.sharedkernal.Common;
using bs.identity.application.Commands.EmployeeEmailConfirmed;
using bs.identity.application.Commands.EmployeePhoneNumberConfirmed;
using bs.identity.application.Commands.EmployeeRegistration;
using bs.identity.domain.Models;
using bs.identity.infrastructure.Persistence.Queries.GetEmployeeInformation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace bs.identity.api.Controllers
{
    public class EmployeeController : BaseController
    {
        [HttpGet("{employeeId}")]
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

        [HttpGet("{employeeId}/PhoneNumberConfirmed")]
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

        [HttpPost("{employeeId}/EmailConfirmed")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EmailConfirmed (Guid employeeId, [FromBody] string token)
        {
            if (employeeId == Guid.Empty)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(new EmployeeEmailConfirmedCommand(employeeId,token)));
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
        public IActionResult EmployeeUpdate([FromRoute] Guid employeeId, [FromBody] EmployeeUpdateRequestDto request)
        {
            if (employeeId == Guid.Empty)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}