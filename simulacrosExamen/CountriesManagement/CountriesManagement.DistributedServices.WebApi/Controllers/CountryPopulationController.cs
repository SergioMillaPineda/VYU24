using CountriesManagement.Domain;
using CountriesManagement.Library.Contracts;
using CountriesManagement.Library.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CountriesManagement.DistributedServices.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryPopulationController : ControllerBase
    {
        private readonly ICountryPopulationService _countryPopulationService;

        public CountryPopulationController(ICountryPopulationService countryPopulationService)
        {
            _countryPopulationService = countryPopulationService;
        }

        [HttpGet("GetByInitialAndYear")]
        public async Task<IActionResult> GetByInitialAndYear([FromQuery] QueryDataDto rqDto)
        {
            GetPopByInitialAndYearRsDto rsDto =
                await _countryPopulationService.GetPopulationByInitialAndYear(rqDto);

            if (rsDto.errors != null)
            {
                return BadRequest(rsDto.errors);
            }
            else if (rsDto.data != null)
            {
                return Ok(MapRsDataToOutputDesiredFormat(rsDto.data, rqDto.Year));
            }
            else
            {
                return BadRequest("Unknown error");
            }
        }

        private static object MapRsDataToOutputDesiredFormat(List<CountryYearPopulation> data, int year)
        {
            return new
            {
                Year = year,
                PopulationByCountry = data.Select(x => new
                {
                    x.Country,
                    x.Population
                })
            };
        }
    }
}
