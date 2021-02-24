using bs.component.sharedkernal.Common;
using bs.identity.application.Commands.TokenAuthenticate;
using bs.identity.application.Commands.TokenRefresh;
using bs.identity.domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace bs.identity.api.Controllers
{
    public class TokenController : BaseController
    {
        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(UserLoginResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Authorize([FromBody] UserLoginRequestDto request)
        {
            return Ok(await _mediator.Send(new TokenAuthenticateCommand(request)));
        }

        [HttpPost("refresh")]
        [ProducesResponseType(typeof(UserLoginResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Refresh([FromBody] string refreshToken)
        {
            return Ok(await _mediator.Send(new TokenRefreshCommand(refreshToken)));
        }
    }
}
