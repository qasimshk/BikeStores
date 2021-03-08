using bs.component.sharedkernal.Common;
using bs.identity.application.Commands.TokenAuthenticate;
using bs.identity.application.Commands.TokenRefresh;
using bs.identity.domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace bs.identity.api.Controllers
{
    public class TokenController : BaseController
    {
        [HttpPost("Authenticate")]
        [ProducesResponseType(typeof(UserLoginResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Authorize([FromBody] UserLoginRequestDto request)
        {
            return Ok(await _mediator.Send(new TokenAuthenticateCommand(request)));
        }

        [HttpPost("Refresh")]
        [ProducesResponseType(typeof(UserLoginResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto request)
        {
            return Ok(await _mediator.Send(new TokenRefreshCommand(request.RefreshToken)));
        }
    }
}
