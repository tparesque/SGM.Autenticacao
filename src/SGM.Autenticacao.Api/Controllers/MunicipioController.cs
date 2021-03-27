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
    [Route("api/municipios")]
    [Authorize]
    [ApiController]
    public class MunicipioController : Controller
    {
        private readonly IMunicipioService _municipioService;

        public MunicipioController(IMunicipioService municipioService)
        {
            _municipioService = municipioService;
        }

        [Authorize(Roles = "Administrador,Gestor,Usuario,Funcionário")]
        [HttpGet("estado/{id}")]
        [ProducesResponseType(typeof(ResultDto<IEnumerable<MunicipioDto>>), 200)]
        public async Task<ResultDto<IEnumerable<MunicipioDto>>> Get(int id)
        {
            return await _municipioService.ObterTodosPorEstado(id);
        } 
    }
}
