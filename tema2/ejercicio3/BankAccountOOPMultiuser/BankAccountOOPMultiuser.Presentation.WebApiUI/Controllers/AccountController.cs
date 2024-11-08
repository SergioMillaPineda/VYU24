using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountOOPMultiuser.Presentation.WebApiUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpGet("GetMoney")]
        public ActionResult GetIncome(decimal income)
        {
            return Ok(0);
        }
    }
}
