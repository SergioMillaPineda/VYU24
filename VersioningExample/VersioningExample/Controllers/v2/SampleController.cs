using Microsoft.AspNetCore.Mvc;

namespace VersioningExample.Controllers.v2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Version 2");
        }
    }
}
