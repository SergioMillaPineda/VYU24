using Microsoft.AspNetCore.Mvc;

namespace VersioningExample.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Version 1");
        }
    }
}
