using bs.component.sharedkernal.Common;
using bs.inventory.application.Commands.AddBasket;
using bs.inventory.domain.Models;
using bs.inventory.infrastructure.Persistence.Queries.GetBasketValidated;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace bs.inventory.api.Controllers
{
    public class BasketController : BaseController
    {
        [HttpGet("{basketRef}/validate")]
        [EnableQuery()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ValidateBasket([FromRoute] Guid basketRef)
        {
            if (basketRef == Guid.Empty) return BadRequest("Invalid basket reference");

            return Ok(await _mediator.Send(new GetBasketValidatedQuery(basketRef)));
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
