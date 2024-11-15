using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversitiesManagement.Services.Contracts;
using UniversitiesManagement.Services.Contracts.Dtos;

namespace UniversitiesManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MigrationsController : ControllerBase
    {
        private readonly IUniversitiesService _universitiesService;

        public MigrationsController(IUniversitiesService universitiesService)
        {
            _universitiesService = universitiesService;
        }

        [HttpGet("Universities")]
        public async Task<IActionResult> MigrateUniversities()
        {
            MigrateAllRsDto result = await _universitiesService.MigrateAllAsync();
            if (result.errors != null)
            {
                return BadRequest(
                    ErrorMessageMapperHelper.MapListAllErrorsEnumToStringMessages(
                        result.errors
                    )
                );
            }

            return Ok();
        }
    }
}
