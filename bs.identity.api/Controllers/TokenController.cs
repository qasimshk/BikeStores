using System;
using bs.component.sharedkernal.Common;
using bs.component.sharedkernal.Exceptions;
using bs.identity.application.Commands.TokenAuthenticate;
using bs.identity.domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace bs.identity.api.Controllers
{
    public class TokenController : BaseController
    {
        [HttpPost("Authorize")]
        [ProducesResponseType(typeof(UserLoginResponseDto), (int)HttpStatusCode.OK)]
        //[ProducesErrorResponseType()]
        public async Task<IActionResult> Authorize([FromBody] UserLoginRequestDto request)
        {
            return Ok(await _mediator.Send(new TokenAuthenticateCommand(request)));
        }
    }
}
