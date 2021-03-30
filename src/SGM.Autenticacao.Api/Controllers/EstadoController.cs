using SGM.Autenticacao.Domain.Dto;
using SGM.Autenticacao.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGM.Autenticacao.Api.Controllers
{
    [EnableCors("ApiCorsPolicy")]
    [Route("api/autenticacao/estados")]
    [Authorize]
    [ApiController]
    public class EstadoController : Controller
    {
        private readonly IEstadoService _estadoService;

        public EstadoController(IEstadoService estadoService)
        {
            _estadoService = estadoService;
        }

        [Authorize(Roles = "Administrador,Gestor,Usuario,Funcionário")]
        [HttpGet("")]
        [ProducesResponseType(typeof(ResultDto<IEnumerable<EstadoDto>>), 200)]
        public async Task<ResultDto<IEnumerable<EstadoDto>>> Get()
        {
            return await _estadoService.ObterTodos();
        } 
    }
}
