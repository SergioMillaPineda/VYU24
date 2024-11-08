using Microsoft.AspNetCore.Mvc;
using WorkersManagementAdminMode.Library.Contracts;
using WorkersManagementAdminMode.Library.Contracts.DTOs;
using WorkersManagementAdminMode.XCutting.Enums;

namespace WorkersManagementAdminMode.DistributedServices.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ITWorkersController : ControllerBase
    {
        private readonly IITWorkerService _itWorkerService;

        public ITWorkersController(IITWorkerService itWorkerService)
        {
            _itWorkerService = itWorkerService;
        }

        [HttpPost("Register")]
        public IActionResult Register(RegisterITWorkerRqDTO dto)
        {
            RegisterITWorkerRsDTO response = _itWorkerService.Register(dto);
            if (response.errors != null && response.errors.HasErrors)
            {
                return (RegisterITWorkerRsErrorsEnum)response.errors.ErrorCodes.First() switch
                {
                    RegisterITWorkerRsErrorsEnum.InvalidLevel => StatusCode(
                                                StatusCodes.Status406NotAcceptable,
                                                "Level field has invalid value. Valid values: [Junior, Middle, Senior]"
                                                ),
                    RegisterITWorkerRsErrorsEnum.CannotWork => StatusCode(
                                                StatusCodes.Status406NotAcceptable,
                                                "ITWorker has to be at least 18 years old to be allowed to work"
                                                ),
                    RegisterITWorkerRsErrorsEnum.DBError => StatusCode(
                                                StatusCodes.Status500InternalServerError,
                                                "Error accessing Database"
                                                ),
                    _ => StatusCode(
                                                StatusCodes.Status500InternalServerError,
                                                "Unknown error"
                                                ),
                };
            }
            else
            {
                return Ok(response.itWorker);
            }
        }
    }
}
