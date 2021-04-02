using bs.component.sharedkernal.Common;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using bs.order.domain.Models;

namespace bs.order.api.Controllers
{
    public class OrderController : BaseController
    {
        [HttpPost("Submit")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public Task<IActionResult> Submit([FromBody] SubmitOrderDto request)
        {
            return default;
        }
    }
}
