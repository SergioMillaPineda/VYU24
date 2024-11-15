using Microsoft.AspNetCore.Mvc;
using SWApi.Enums;
using SWApi.Services.Contracts;
using SWApi.Services.Contracts.Dtos;

namespace SWApi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanetsController : ControllerBase
    {
        private readonly IPlanetsService _planetsService;

        public PlanetsController(IPlanetsService planetsService)
        {
            _planetsService = planetsService;
        }

        [HttpGet("Refresh")]
        public async Task<IActionResult> Refresh()
        {
            RefreshPlanetsRsDto result = await _planetsService.RefreshPlanets();
            if (result.errors != null)
            {
                return BadRequest(result.errors.Select(MapErrorEnumToString));
            }
            RefreshPlanetsErrorEnum.SWApiErrorConnection.ToString();
            return Ok(result.data);
        }

        private static string MapErrorEnumToString(RefreshPlanetsErrorEnum errorEnum)
        {
            return errorEnum switch
            {
                RefreshPlanetsErrorEnum.SWApiErrorConnection => "Error while connecting to SWApi",
                RefreshPlanetsErrorEnum.EntityMappingConnection => "Error while mapping SWApi data to save it in database",
                RefreshPlanetsErrorEnum.SWDbErrorConnection => "Error while connecting to SWDB",
                _ => "unknown error",
            };
        }
    }
}
