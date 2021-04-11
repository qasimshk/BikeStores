using bs.component.sharedkernal.Common;
using bs.inventory.domain.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace bs.inventory.api.Controllers
{
    public class StoreController : BaseController
    {
        [HttpGet("{storeId}")]
        [EnableQuery()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetStore([FromRoute] int storeId)
        {
            return Ok();
        }

        [HttpGet("{Search}")]
        [EnableQuery()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> SearchStore([FromQuery] StoreFilterDto filter)
        {
            return Ok();
        }
    }
}
