using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace SGM.Autenticacao.Api.Controllers
{
    [EnableCors("ApiCorsPolicy")]
    [Route("api/home")]
    [Authorize]
    [ApiController]
    public class HomeController : Controller
    {       
        public HomeController()
        {           
        }       
    }
}
