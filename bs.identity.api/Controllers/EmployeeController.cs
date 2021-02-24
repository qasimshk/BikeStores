using bs.component.sharedkernal.Common;
using bs.identity.application.Commands.EmployeeRegistration;
using bs.identity.domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace bs.identity.api.Controllers
{
    public class EmployeeController : BaseController
    {
        [HttpGet("all")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult All([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            /* Example for paging
               var itemsOnPage = await _catalogContext.CatalogItems
                .Where(c => c.Name.StartsWith(name))
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
             */

            return Ok();
        }

        [HttpGet("{employeeId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Employee(Guid employeeId)
        {
            return Ok();
        }
        
        [HttpGet("search")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Employee([FromQuery] EmployeeFilter filter)
        {
            // in store case filter how many employee are working in the perticular store
            return Ok();
        }

        [HttpPost("register")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadGateway)]
        public async Task<IActionResult> Register([FromBody] EmployeeRegisterRequestDto request)
        {
            return Ok(await _mediator.Send(_mapper.Map<EmployeeRegistrationCommand>(request)));
        }

        [HttpPut("{employeeId}/update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadGateway)]
        public IActionResult EmployeeUpdate([FromRoute] Guid employeeId)
        {
            return Ok();
        }
    }
}
