using bs.component.sharedkernal.Common;
using bs.identity.application.Commands.EmployeeRegistration;
using bs.identity.domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bs.identity.api.Controllers
{
    public class AccountController : BaseController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] EmployeeRegisterRequestDto request)
        {
            return Ok(await _mediator.Send(_mapper.Map<EmployeeRegistrationCommand>(request)));
            //return Ok(await _mediator.Send(new EmployeeRegistrationCommand(request)));

            
        }
    }
}
