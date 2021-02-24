using System;
using bs.component.sharedkernal.Common;
using bs.identity.application.Commands.EmployeeRegistration;
using bs.identity.domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace bs.identity.api.Controllers
{
    public class EmployeeController : BaseController
    {
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult All()
        {
            // add paging to it
            return Ok();
        }

        [HttpGet("{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Employee(Guid employeeId)
        {
            return Ok();
        }
        
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Employee([FromQuery] EmployeeFilter filter)
        {
            // in store case filter how many employee are working in the perticular store
            return Ok();
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] EmployeeRegisterRequestDto request)
        {
            return Ok(await _mediator.Send(_mapper.Map<EmployeeRegistrationCommand>(request)));
        }

        [HttpPut("{employeeId}/update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EmployeeUpdate([FromRoute] Guid employeeId)
        {
            return Ok();
        }
    }
}
