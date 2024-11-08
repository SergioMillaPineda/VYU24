using Microsoft.AspNetCore.Mvc;

namespace VersioningExample.Controllers.v3
{
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Version 3");
        }
    }
}
