using bs.component.sharedkernal.Common;
using bs.inventory.domain.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace bs.inventory.api.Controllers
{
    public class CategoryController : BaseController
    {
        [HttpGet("{categoryId}")]
        [EnableQuery()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCategory([FromRoute] int categoryId)
        {
            return Ok();
        }

        [HttpGet("{Search}")]
        [EnableQuery()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> SearchCategory([FromQuery] string filter)
        {
            return Ok();
        }
        
        [HttpPost("Add")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadGateway)]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryDto request)
        {
            return Ok();
        }
    }
}
