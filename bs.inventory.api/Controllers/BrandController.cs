using bs.component.sharedkernal.Common;
using bs.inventory.domain.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace bs.inventory.api.Controllers
{
    public class BrandController : BaseController
    {
        [HttpGet("{brandId}")]
        [EnableQuery()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetBrand([FromRoute] int brandId)
        {
            return Ok();
        }

        [HttpGet("{Search}")]
        [EnableQuery()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> SearchBrand([FromQuery] string filter)
        {
            return Ok();
        }

        [HttpPost("Add")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadGateway)]
        public async Task<IActionResult> AddBrand([FromBody] AddBrandDto request)
        {
            return Ok();
        }
    }
}
