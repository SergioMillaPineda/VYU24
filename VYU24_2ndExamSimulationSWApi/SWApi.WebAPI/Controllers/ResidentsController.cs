using Microsoft.AspNetCore.Mvc;
using SWApi.Services.Contracts;
using SWApi.Services.Contracts.Dtos;

namespace SWApi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidentsController : ControllerBase
    {
        private readonly IResidentsService _residentsService;

        public ResidentsController(IResidentsService residentsService)
        {
            _residentsService = residentsService;
        }

        [HttpGet("GetResidentsByPlanetName")]
        public async Task<IActionResult> GetResidentsByPlanetName(string planetName)
        {
            GetResidentsByPlanetNameRsDto result = await _residentsService.GetResidentsByPlanetName(planetName);
            if (result != null && result.data != null)
            {
                return Ok(result.data);
            }
            if (result != null && result.errors != null)
            {
                return BadRequest(result.errors);
            }

            return BadRequest("unknown error");
        }
    }
}
