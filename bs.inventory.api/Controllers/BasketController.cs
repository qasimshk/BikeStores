using System;
using bs.component.sharedkernal.Common;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using bs.inventory.application.Commands.AddBasket;
using bs.inventory.domain.Models;

namespace bs.inventory.api.Controllers
{
    public class BasketController : BaseController
    {
        [HttpGet("{BasketRef}/Validate")]
        [EnableQuery()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ValidateBasket([FromRoute] Guid basketRef)
        {
            return Ok();
        }

        [HttpPost("Add")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadGateway)]
        public async Task<IActionResult> AddBasket([FromBody] AddBasketDto request)
        {
            return Ok(await _mediator.Send(new AddBasketCommand(request)));
        }
    }
}
