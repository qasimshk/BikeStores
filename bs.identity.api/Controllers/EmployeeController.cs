using System;
using bs.component.sharedkernal.Common;
using bs.identity.application.Commands.EmployeeRegistration;
using bs.identity.domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bs.identity.api.Controllers
{
    public class EmployeeController : BaseController
    {
        [HttpGet("all")]
        public IActionResult All()
        {
            // add paging to it
            return Ok();
        }

        [HttpGet("{employeeId}")]
        public IActionResult Employee(Guid employeeId)
        {
            return Ok();
        }
        
        [HttpGet("search")]
        public IActionResult Employee([FromQuery] EmployeeFilter filter)
        {
            // in store case filter how many employee are working in the perticular store
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] EmployeeRegisterRequestDto request)
        {
            return Ok(await _mediator.Send(_mapper.Map<EmployeeRegistrationCommand>(request)));
        }

        [HttpPut("{employeeId}/update")]
        public IActionResult EmployeeUpdate([FromRoute] Guid employeeId)
        {
            return Ok();
        }
    }
}
