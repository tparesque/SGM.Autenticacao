using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace SGM.Autenticacao.Api.Controllers
{
    [EnableCors("ApiCorsPolicy")]
    [Route("api/autenticacao/health-check")]
    [ApiController]
    public class HealthCheckController : Controller
    {       
        [HttpGet]
        public IActionResult HealthCheck()
		{
            return Ok();
		}
    }
}
