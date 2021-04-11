using System;
using bs.component.sharedkernal.Common;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace bs.inventory.api.Controllers
{
    public class ProductController : BaseController
    {
        [HttpGet("{productId}")]
        [EnableQuery()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProduct([FromRoute] int productId)
        {
            return Ok();
        }

        [HttpGet("{Search}")]
        [EnableQuery()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> SearchProduct([FromQuery] object filter)
        {
            return Ok();
        }

        [HttpPost("Add")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadGateway)]
        public async Task<IActionResult> AddProduct([FromBody] object request)
        {
            return Ok();
        }

        [HttpPost("{productRef}/AddStock")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadGateway)]
        public async Task<IActionResult> AddStock([FromRoute] Guid productRef, [FromBody] object request)
        {
            return Ok();
        }
    }
}
