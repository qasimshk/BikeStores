using bs.component.sharedkernal.Common;
using bs.order.application.Commands.SubmitOrder;
using bs.order.domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace bs.order.api.Controllers
{
    public class OrderController : BaseController
    {
        [HttpPost("Submit")]
        [ProducesResponseType(typeof(SubmitOrderResultDto),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Submit([FromBody] SubmitOrderDto request)
        {
            return Ok(await _mediator.Send(new SubmitOrderCommand(request)));
        }
    }
}
