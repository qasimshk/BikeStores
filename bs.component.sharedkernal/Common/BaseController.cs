using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bs.component.sharedkernal.Common
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public abstract class BaseController : ControllerBase { }
}
